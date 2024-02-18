using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients;

/// <summary>
/// Represents a repository for managing clients.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Retrieves a client by its identifier and realm asynchronously.
    /// </summary>
    /// <param name="clientId">The identifier of the client.</param>
    /// <param name="realmId">The identifier of the realm.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the client if found; otherwise, null.</returns>
    Task<Client?> GetByClientIdAndRealmId(ClientId clientId, RealmId realmId, CancellationToken ct = default);
}