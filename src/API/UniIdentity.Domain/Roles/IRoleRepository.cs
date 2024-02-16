namespace UniIdentity.Domain.Roles;

/// <summary>
/// Represents a repository for managing roles.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Retrieves a role by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the role if found; otherwise, null.</returns>
    Task<Role?> GetByIdAsync(RoleId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a role by its name asynchronously.
    /// </summary>
    /// <param name="name">The name of the role.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the role if found; otherwise, null.</returns>
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}