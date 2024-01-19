using UniIdentity.Domain.Roles;

namespace UniIdentity.Domain.Users;

public sealed class UserRole
{
    public UserId UserId { get; private set; }
    public RoleId RoleId { get; private set; }
    public User User { get; private set; } = null!;
    public Role Role { get; private set; } = null!;

    private UserRole(UserId userId, RoleId roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public static UserRole Create(UserId userId, RoleId roleId)
    {
        var userRole = new UserRole(userId, roleId); 
        return userRole;
    }
}