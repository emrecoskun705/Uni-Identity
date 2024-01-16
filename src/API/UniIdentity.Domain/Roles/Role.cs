using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Roles.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Roles;

public sealed class Role : BaseEntity<RoleId>
{
    public const string DefaultUser = nameof(DefaultUser); 
    public const string Administrator = nameof(Administrator); 
    public RoleId Id { get; private set; }
    public Name Name { get; private set; }
    public NormalizedName NormalizedName { get; private set; }
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
        NormalizedName = NormalizedName.Create(name.Value);
        RealmId = realmId;
        ClientId = clientId;
    }

    public static Role Create(RoleId roleId, Name name, RealmId realmId, ClientId clientId)
    {
        return new Role(roleId, name, realmId, clientId);
    }
}