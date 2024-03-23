namespace UniIdentity.Domain.Scopes.Repositories;

/// <summary>
/// Represents a repository interface for adding default scopes.
/// </summary>
public interface IAddDefaultScopeRepository
{
    /// <summary>
    /// Adds a default scope to the repository.
    /// </summary>
    /// <param name="defaultScope">The default scope to add.</param>
    void Add(DefaultScope defaultScope);
}