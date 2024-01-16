using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Roles.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Roles;

public sealed class Role : BaseEntity<RoleId>
{
    public RoleId Id { get; private set; }
    public Name Name { get; private set; }
    public ICollection<UserRole>? UserRoles { get; private set; }

    private Role(RoleId id, Name name)
    {
        Id = id;
        Name = name;
    }

    public static Role Create(RoleId roleId, Name name, RealmId realmId, ClientId clientId)
    {
        return new Role(roleId, name);
    }
}