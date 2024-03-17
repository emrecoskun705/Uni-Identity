using UniIdentity.Domain.Roles;

namespace UniIdentity.Domain.Users;

/// <summary>
/// Represents an association between a user and a role within the UniIdentity domain.
/// </summary>
public sealed class UserRole
{
    /// <summary>
    /// Gets the unique identifier of the user associated with the role.
    /// </summary>
    public UserId UserId { get; }

    /// <summary>
    /// Gets the unique identifier of the role associated with the user.
    /// </summary>
    public RoleId RoleId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRole"/> class with the specified user and role identifiers.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="roleId">The unique identifier of the role.</param>
    public UserRole(UserId userId, RoleId roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
    
}