using UniIdentity.Application.Contracts.Messaging;
using UniIdentity.Domain;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Clients.Services;
using UniIdentity.Domain.ClientScopes.Repositories;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms.Repositories;
using UniIdentity.Domain.Scopes.Repositories;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Clients.Commands.AddClientRequest;

internal sealed class AddClientRequestCommandHandler : ICommandHandler<AddClientRequestCommand, Result>
{
    private readonly IClientExistenceRepository _clientExistenceRepository;
    private readonly IAddClientRepository _addClientRepository;
    private readonly IAddClientScopeRepository _addClientScopeRepository;
    private readonly IGetDefaultScopeRepository _getDefaultScopeRepository;
    private readonly IRealmExistenceRepository _realmExistenceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddClientRequestCommandHandler(
        IClientExistenceRepository clientExistenceRepository, 
        IAddClientRepository addClientRepository,
        IUnitOfWork unitOfWork, 
        IGetDefaultScopeRepository getDefaultScopeRepository, 
        IAddClientScopeRepository addClientScopeRepository, 
        IRealmExistenceRepository realmExistenceRepository)
    {
        _clientExistenceRepository = clientExistenceRepository;
        _addClientRepository = addClientRepository;
        _unitOfWork = unitOfWork;
        _getDefaultScopeRepository = getDefaultScopeRepository;
        _addClientScopeRepository = addClientScopeRepository;
        _realmExistenceRepository = realmExistenceRepository;
    }

    public async Task<Result> Handle(AddClientRequestCommand request, CancellationToken cancellationToken)
    {
        var clientExists = await _clientExistenceRepository.CheckAsync(request.RealmId, request.ClientKey);
        var realmExists = await _realmExistenceRepository.CheckAsync(request.RealmId);

        if (!realmExists)
            return Result.Failure(DomainErrors.RealmErrors.NotFound);

        if (clientExists)
            return Result.Failure(DomainErrors.ClientErrors.AlreadyExists);

        var clientCreateService = new ClientCreateService(_addClientRepository, _addClientScopeRepository,
            _getDefaultScopeRepository);

        await clientCreateService.AddClientWithDefaults(request.RealmId, request.ClientKey, request.Protocol, request.RootUrl,
            cancellationToken: cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}