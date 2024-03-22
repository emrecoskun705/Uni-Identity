using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients.Repositories;

/// <summary>
/// Represents a repository interface for checking the existence of clients.
/// </summary>
public interface IClientExistenceRepository
{
    /// <summary>
    /// Checks if a client with the specified client ID exists.
    /// </summary>
    /// <param name="clientId">The ID of the client to check.</param>
    /// <returns>The task result indicates whether the client exists.</returns>
    Task<bool> CheckAsync(ClientId clientId);
    
    /// <summary>
    /// Checks if a client with the specified realm ID and client key exists.
    /// </summary>
    /// <param name="realmId">The ID of the realm to which the client belongs.</param>
    /// <param name="clientKey">The key of the client to check.</param>
    /// <returns> The task result indicates whether the client exists.</returns>
    Task<bool> CheckAsync(RealmId realmId, ClientKey clientKey);
}