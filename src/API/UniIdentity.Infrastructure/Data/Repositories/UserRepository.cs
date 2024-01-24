using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Users;
using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default)
    {
        return await _db.User.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task AddRoleAsync(UserRole role)
    {
        await _db.UserRole.AddAsync(role);
    }

    public async Task<User?> GetByUsernameAsync(Username username, CancellationToken ct = default)
    {
        return await _db.User.FirstOrDefaultAsync(
            x => x.NormalizedUsername == NormalizedUsername.Create(username.Value), ct);
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default)
    {
        return await _db.User.FirstOrDefaultAsync(
            x => x.NormalizedEmail == NormalizedEmail.Create(email.Value), ct);
    }
}