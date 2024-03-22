namespace UniIdentity.Domain.Realms.Repositories;

/// <summary>
/// Represents a repository interface for checking the existence of realms.
/// </summary>
public interface IRealmExistenceRepository
{
    /// <summary>
    /// Checks if a realm with the specified realm ID exists.
    /// </summary>
    /// <param name="realmId">The ID of the realm to check.</param>
    /// <returns>The task result indicates whether the realm exists.</returns>
    Task<bool> CheckAsync(RealmId realmId);
}