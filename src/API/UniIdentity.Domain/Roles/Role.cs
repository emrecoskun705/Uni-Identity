using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Roles.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Roles;

public sealed class Role : BaseEntity<RoleId>
{
    public Name Name { get; private set; }
    public string ClientRealmConstraint { get; private set; }
    public bool IsClientRole { get; private set; }
    public RealmId RealmId { get; private set; }
    public ClientId? ClientId { get; private set; }
    
    public Realm? Realm { get; private set; }
    public Client? Client { get; private set; }
    public ICollection<UserRole>? UserRoles { get; private set; }

    private Role(RoleId id, Name name, RealmId realmId, ClientId clientId)
    {
        Id = id;
        Name = name;
        RealmId = realmId;
        ClientId = clientId;
    }
    
    private Role(RoleId id, Name name, RealmId realmId)
    {
        Id = id;
        Name = name;
        RealmId = realmId;
        ClientId = null;
    }

    public static Role CreateRealmRole(Name name, RealmId realmId)
    {
        return new Role(RoleId.New(), name, realmId);
    }
    
    public static Role CreateRealmRole(RoleId roleId, Name name, RealmId realmId)
    {
        return new Role(roleId, name, realmId);
    }
    
    public static Role CreateClientRole(Name name, RealmId realmId, ClientId clientId)
    {
        return new Role(RoleId.New(), name, realmId, clientId);
    }
    
    public static Role CreateClientRole(RoleId roleId, Name name, RealmId realmId, ClientId clientId)
    {
        return new Role(roleId, name, realmId, clientId);
    }
}