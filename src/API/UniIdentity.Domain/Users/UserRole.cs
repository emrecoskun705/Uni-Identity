using UniIdentity.Domain.Roles;

namespace UniIdentity.Domain.Users;

public sealed class UserRole
{
    public UserId UserId { get; private set; }
    public RoleId RoleId { get; private set; }

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