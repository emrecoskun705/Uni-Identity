using UniIdentity.Domain.Users;

namespace UniIdentity.Application.Contracts.Sessions;

public interface ISessionManager
{
    Task<SessionKey> CreateUserSession(UserId userId);
    Task RemoveUserSession(UserId userId, SessionKey sessionKey);
}