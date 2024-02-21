﻿namespace UniIdentity.Domain.Realms;

/// <summary>
/// Represents a repository for managing realms.
/// </summary>
public interface IRealmRepository
{
    /// <summary>
    /// Retrieves a realm by its unique identifier asynchronously.
    /// </summary>
    /// <param name="realmId">The unique identifier of the realm.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the realm if found; otherwise, null.</returns>
    Task<Realm?> GetByRealmId(RealmId realmId, CancellationToken ct = default);

    /// <summary>
    /// Retrieves the attributes associated with a realm asynchronously.
    /// </summary>
    /// <param name="realmId">The unique identifier of the realm.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The task result contains a collection of realm attributes if found; otherwise, an empty collection.</returns>

    Task<IEnumerable<RealmAttribute>> GetRealmAttributesAsync(RealmId realmId, CancellationToken ct = default);
}