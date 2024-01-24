using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default);

    void Add(User user);
    Task AddRoleAsync(UserRole role);
    Task<User?> GetByUsernameAsync(Username username, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
}