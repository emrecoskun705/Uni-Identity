using UniIdentity.Domain.Clients.ValueObjects;

namespace UniIdentity.Domain.ClientScopes.Repositories;

public interface IGetClientScopeRepository
{
    Task<IReadOnlyCollection<ClientScope>> GetByClientId(ClientId clientId);
}