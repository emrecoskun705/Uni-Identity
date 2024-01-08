using UniIdentity.Domain.Common;
using UniIdentity.Domain.Roles.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Roles;

public sealed class Role : BaseEntity<RoleId>
{
    public const string DefaultUser = nameof(DefaultUser); 
    public const string Administrator = nameof(Administrator); 
    public RoleId Id { get; set; }
    public Name Name { get; set; }
    public NormalizedName NormalizedName { get; set; }
    public ICollection<UserRole>? UserRoles { get; private set; }

    private Role(RoleId id, Name name)
    {
        Id = id;
        Name = name;
        NormalizedName = NormalizedName.Create(name.Value);
    }

    public static Role Create(RoleId roleId, Name name)
    {
        return new Role(roleId, name);
    }
}