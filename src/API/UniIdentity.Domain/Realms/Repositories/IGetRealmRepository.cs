﻿namespace UniIdentity.Domain.Realms.Repositories;

public interface IGetRealmRepository
{
    /// <summary>
    /// Retrieves a realm by its unique identifier asynchronously.
    /// </summary>
    /// <param name="realmId">The unique identifier of the realm.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the realm if found; otherwise, null.</returns>
    Task<Realm?> GetByRealmId(RealmId realmId, CancellationToken ct = default);
}