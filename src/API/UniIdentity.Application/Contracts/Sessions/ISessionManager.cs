using UniIdentity.Domain.Users;

namespace UniIdentity.Application.Contracts.Sessions;

/// <summary>
/// Manages user sessions within the application. This component facilitates the creation,
/// removal, and checking of user sessions to maintain user authentication and activity state.
/// </summary>
public interface ISessionManager
{
    /// <summary>
    /// Creates a new session for the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user for whom the session is created.</param>
    /// <returns>The session key associated with the newly created session.</returns>
    Task<SessionKey> CreateUserSession(UserId userId);
        
    /// <summary>
    /// Removes the specified session for the user.
    /// </summary>
    /// <param name="userId">The ID of the user whose session is to be removed.</param>
    /// <param name="sessionKey">The session key to be removed.</param>
    /// <returns>An asynchronous task representing the operation.</returns>
    Task RemoveUserSession(UserId userId, SessionKey sessionKey);
        
    /// <summary>
    /// Checks if a session exists for the specified user with the given session key.
    /// </summary>
    /// <param name="userId">The ID of the user to check the session for.</param>
    /// <param name="sessionKey">The session key to check.</param>
    /// <returns>True if the session exists for the user with the provided session key, otherwise false.</returns>
    Task<bool> CheckUserSessionExists(UserId userId, SessionKey sessionKey);
}