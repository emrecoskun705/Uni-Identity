using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients.Repositories;

public interface IGetClientRepository
{
    /// <summary>
    /// Retrieves a client by its identifier and realm asynchronously.
    /// </summary>
    /// <param name="clientKey">The identifier of the client.</param>
    /// <param name="realmId">The identifier of the realm.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the client if found; otherwise, null.</returns>
    Task<Client?> GetByClientKeyAndRealmIdAsync(ClientKey clientKey, RealmId realmId, CancellationToken ct = default);
}