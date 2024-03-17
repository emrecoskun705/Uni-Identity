using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.RealmAttributes.Repositories;

public interface IGetRealmAttributeRepository
{
    /// <summary>
    /// Retrieves a specific attribute of a realm by its unique identifier and attribute name asynchronously.
    /// </summary>
    /// <param name="realmId">The unique identifier of the realm.</param>
    /// <param name="name">The name of the attribute to retrieve.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The task result contains the realm attribute if found; otherwise, an exception is thrown.</returns>
    Task<RealmAttribute> GetByNameAsync(RealmId realmId, string name, CancellationToken ct = default);
}