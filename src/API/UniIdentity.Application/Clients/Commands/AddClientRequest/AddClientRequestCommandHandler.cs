using UniIdentity.Application.Contracts.Messaging;
using UniIdentity.Domain;
using UniIdentity.Domain.ClientAttributes.Consts;
using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Clients.Commands.AddClientRequest;

internal sealed class AddClientRequestCommandHandler : ICommandHandler<AddClientRequestCommand, Result>
{
    private readonly IClientExistenceRepository _clientExistenceRepository;
    private readonly IAddClientRepository _addClientRepository;
    private readonly IAddClientAttributeRepository _addClientAttributeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddClientRequestCommandHandler(
        IClientExistenceRepository clientExistenceRepository, 
        IAddClientRepository addClientRepository,
        IAddClientAttributeRepository addClientAttributeRepository,
        IUnitOfWork unitOfWork)
    {
        _clientExistenceRepository = clientExistenceRepository;
        _addClientRepository = addClientRepository;
        _unitOfWork = unitOfWork;
        _addClientAttributeRepository = addClientAttributeRepository;
    }

    public async Task<Result<Result>> Handle(AddClientRequestCommand request, CancellationToken cancellationToken)
    {
        var clientExists = await _clientExistenceRepository.CheckAsync(request.RealmId, request.ClientKey);

        if (clientExists)
            return Result.Failure(DomainErrors.ClientErrors.AlreadyExists);

        var client = Client.Create(request.RealmId, request.ClientKey, request.Protocol, request.RootUrl);
        
        _addClientRepository.Add(client);

        var useRefreshTokenAttribute = client.CreateAttribute(
            ClientAttributeName.EnableRefreshToken, 
            true.ToString());

        _addClientAttributeRepository.Add(useRefreshTokenAttribute);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}