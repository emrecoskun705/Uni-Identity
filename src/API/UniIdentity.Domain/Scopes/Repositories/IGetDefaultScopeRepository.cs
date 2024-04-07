using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Scopes.Repositories;

/// <summary>
/// Represents a repository interface for retrieving default scopes associated with a realm.
/// </summary>
public interface IGetDefaultScopeRepository
{
    /// <summary>
    /// Retrieves all default scopes associated with the specified realm ID.
    /// </summary>
    /// <param name="realmId">The ID of the realm to retrieve default scopes for.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of default scopes.</returns>
    Task<IEnumerable<DefaultScope>> GetAllByRealmId(RealmId realmId, CancellationToken cancellationToken = default);
}