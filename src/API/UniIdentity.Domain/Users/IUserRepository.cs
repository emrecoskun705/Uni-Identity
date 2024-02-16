using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Domain.Users;

/// <summary>
/// Represents a repository for managing user data.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user if found; otherwise, null.</returns>
    Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default);

    /// <summary>
    /// Adds a new user asynchronously.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(User user);

    /// <summary>
    /// Adds a role to a user asynchronously.
    /// </summary>
    /// <param name="role">The role to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddRoleAsync(UserRole role);

    /// <summary>
    /// Retrieves a user by their username asynchronously.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user if found; otherwise, null.</returns>
    Task<User?> GetByUsernameAsync(Username username, CancellationToken ct = default);

    /// <summary>
    /// Retrieves a user by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user if found; otherwise, null.</returns>
    Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
}