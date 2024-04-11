namespace UniIdentity.Domain.Configs.Repositories;

/// <summary>
/// Represents a repository interface for adding configurations.
/// </summary>
public interface IAddConfigRepository
{
    /// <summary>
    /// Adds a new configuration to the repository.
    /// </summary>
    /// <param name="config">The configuration to be added.</param>
    void Add(Config config);
}