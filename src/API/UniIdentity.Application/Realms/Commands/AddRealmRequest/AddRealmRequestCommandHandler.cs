using UniIdentity.Application.Contracts.Messaging;
using UniIdentity.Domain;
using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.ClientScopes.Repositories;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Configs.Enums;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Realms.Consts;
using UniIdentity.Domain.Realms.Repositories;
using UniIdentity.Domain.Scopes;
using UniIdentity.Domain.Scopes.Repositories;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Realms.Commands.AddRealmRequest;

internal sealed class AddRealmRequestCommandHandler : ICommandHandler<AddRealmRequestCommand>
{
    private readonly IGetRealmRepository _getRealmRepository;
    private readonly IAddRealmRepository _addRealmRepository;
    private readonly IAddClientRepository _addClientRepository;
    private readonly IAddClientAttributeRepository _addClientAttributeRepository;
    private readonly IAddClientScopeRepository _addClientScopeRepository;
    private readonly IAddDefaultScopeRepository _addDefaultScopeRepository;
    private readonly IAddRealmAttributeRepository _addRealmAttributeRepository;
    private readonly IAddScopeRepository _addScopeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddRealmRequestCommandHandler(
        IGetRealmRepository getRealmRepository, 
        IAddRealmRepository addRealmRepository, 
        IAddClientRepository addClientRepository,
        IAddClientAttributeRepository addClientAttributeRepository,
        IAddClientScopeRepository addClientScopeRepository,
        IAddDefaultScopeRepository addDefaultScopeRepository,
        IAddRealmAttributeRepository addRealmAttributeRepository, 
        IAddScopeRepository addScopeRepository,
        IUnitOfWork unitOfWork)
    {
        _getRealmRepository = getRealmRepository;
        _addRealmRepository = addRealmRepository;
        _addClientRepository = addClientRepository;
        _addClientAttributeRepository = addClientAttributeRepository;
        _addClientScopeRepository = addClientScopeRepository;
        _addDefaultScopeRepository = addDefaultScopeRepository;
        _addRealmAttributeRepository = addRealmAttributeRepository;
        _addScopeRepository = addScopeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddRealmRequestCommand request, CancellationToken cancellationToken)
    {
        var realmExists = (await _getRealmRepository.GetByRealmId(new RealmId(request.Name), cancellationToken)) != null;

        if (realmExists)
            return Result.Failure(DomainErrors.RealmErrors.AlreadyExists);

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var realm = Realm.Create(request.Name, request.Enabled);

            _addRealmRepository.Add(realm);

            AddDefaultAttributes(realm);

            var scopes = AddRealmScopes(realm);

            var defaultScopes = AddRealmDefaultScopes(realm, scopes);

            var clients = AddDefaultClients(realm);

            AddDefaultClientScopes(clients, defaultScopes);

            RsaGenerationConfig.CreateWithConfigurations(realm.Id, "default-rsa");

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception)
        {
           await transaction.RollbackAsync(cancellationToken);
           throw;
        }
        
        return Result.Success();
    }

    private void AddDefaultClientScopes(IEnumerable<Client> clients, IReadOnlyList<DefaultScope> defaultScopes)
    {
        foreach (var client in clients)
        {
            client.AddDefaultClientScopes(_addClientScopeRepository, defaultScopes);
        }
    }

    private IReadOnlyList<Scope> AddRealmScopes(Realm realm)
    {
        return realm.AddScopes(_addScopeRepository);
    }

    private IReadOnlyList<DefaultScope> AddRealmDefaultScopes(Realm realm, IReadOnlyList<Scope> scopes)
    {
        return realm.AddDefaultScopes(_addDefaultScopeRepository, scopes);
    }

    private void AddDefaultAttributes(Realm realm)
    {
        var defaultSignatureAlgorithm =
            realm.CreateAttribute(RealmAttributeName.SignatureAlgorithm, SignatureAlg.Default);
        
        _addRealmAttributeRepository.Add(defaultSignatureAlgorithm);
    }

    private List<Client> AddDefaultClients(Realm realm)
    {
        var clients = new List<Client>();
        
        var addAccountClient = Client.Create(realm.Id, ClientKey.FromValue("account"), Protocol.OpenIdConnect,
            "/realms/" + realm.Id + "/account/");
        
        var addAdminClient = Client.Create(realm.Id, ClientKey.FromValue("admin"), Protocol.OpenIdConnect,
            "/admin/" + realm.Id + "/console/");
        
        clients.Add(addAccountClient);
        clients.Add(addAdminClient);
        
        _addClientRepository.Add(addAccountClient);
        _addClientRepository.Add(addAdminClient);

        addAccountClient.AddDefaultClientAttributes(_addClientAttributeRepository);
        addAdminClient.AddDefaultClientAttributes(_addClientAttributeRepository);

        return clients;
    }
}