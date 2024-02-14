using UniIdentity.Domain.Users;

namespace UniIdentity.Application.Contracts.Sessions;

public interface ISessionManager
{
    Task<SessionKey> CreateUserSession(User user);
}