using UniIdentity.Application.Contracts.Messaging;
using UniIdentity.Domain;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Clients.Services;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.ClientScopes.Repositories;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Configs.Repositories;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Realms.Repositories;
using UniIdentity.Domain.Realms.Services;
using UniIdentity.Domain.Scopes.Repositories;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Realms.Commands.AddRealmRequest;

internal sealed class AddRealmRequestCommandHandler : ICommandHandler<AddRealmRequestCommand, Result>
{
    private readonly IGetRealmRepository _getRealmRepository;
    private readonly IAddRealmRepository _addRealmRepository;
    private readonly IAddClientRepository _addClientRepository;
    private readonly IAddClientScopeRepository _addClientScopeRepository;
    private readonly IAddConfigRepository _addConfigRepository;
    private readonly IAddDefaultScopeRepository _addDefaultScopeRepository;
    private readonly IAddRealmAttributeRepository _addRealmAttributeRepository;
    private readonly IAddScopeRepository _addScopeRepository;
    private readonly IGetDefaultScopeRepository _getDefaultScopeRepository;
    private readonly IRealmExistenceRepository _realmExistenceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddRealmRequestCommandHandler(
        IGetRealmRepository getRealmRepository, 
        IAddRealmRepository addRealmRepository, 
        IAddClientRepository addClientRepository,
        IAddClientScopeRepository addClientScopeRepository,
        IAddConfigRepository addConfigRepository,
        IAddDefaultScopeRepository addDefaultScopeRepository,
        IAddRealmAttributeRepository addRealmAttributeRepository, 
        IAddScopeRepository addScopeRepository,
        IGetDefaultScopeRepository getDefaultScopeRepository,
        IUnitOfWork unitOfWork, IRealmExistenceRepository realmExistenceRepository)
    {
        _getRealmRepository = getRealmRepository;
        _addRealmRepository = addRealmRepository;
        _addClientRepository = addClientRepository;
        _addClientScopeRepository = addClientScopeRepository;
        _addConfigRepository = addConfigRepository;
        _addDefaultScopeRepository = addDefaultScopeRepository;
        _addRealmAttributeRepository = addRealmAttributeRepository;
        _addScopeRepository = addScopeRepository;
        _getDefaultScopeRepository = getDefaultScopeRepository;
        _unitOfWork = unitOfWork;
        _realmExistenceRepository = realmExistenceRepository;
    }

    public async Task<Result> Handle(AddRealmRequestCommand request, CancellationToken cancellationToken)
    {
        var realmExists = await _realmExistenceRepository.CheckAsync(new RealmId(request.Name));

        if (realmExists)
            return Result.Failure(DomainErrors.RealmErrors.AlreadyExists);

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var realmCreateService = new RealmCreateService(_addRealmRepository, _addRealmAttributeRepository,
                _addScopeRepository, _addDefaultScopeRepository, _addConfigRepository);
            
            var clientCreateService = new ClientCreateService(_addClientRepository, _addClientScopeRepository,
                _getDefaultScopeRepository);
            
            #region Realm Create
            
            var realmRepresentation = realmCreateService.AddRealmWithDefaults(request.Name, request.Enabled);
            var realm = realmRepresentation.Realm;

            #endregion
            // save it because we would need data while creating client 
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            #region Client Create

            await clientCreateService.AddClientWithDefaults(realm.Id, ClientKey.FromValue("account"), Protocol.OpenIdConnect,
                "/realms/" + realm.Id + "/account/", cancellationToken: cancellationToken);

            await clientCreateService.AddClientWithDefaults(realm.Id, ClientKey.FromValue("admin"), Protocol.OpenIdConnect,
                "/admin/" + realm.Id + "/console/", cancellationToken);
            
            #endregion

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
    
}