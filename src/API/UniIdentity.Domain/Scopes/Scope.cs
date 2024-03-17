using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Scopes;

public sealed class Scope : BaseEntity
{
    public ScopeId Id { get; private set; }
    public string Name { get; private set; }
    public string Protocol { get; private set; }
    public RealmId RealmId { get; private set; }
    public string Description { get; private set; }

    private Scope(ScopeId id, string name, string protocol, RealmId realmId, string description)
    {
        Id = id;
        Name = name;
        Protocol = protocol;
        RealmId = realmId;
        RealmId = realmId;
        Description = description;
    }

    public static Scope Create(string name, string protocol, RealmId realmId, string description)
    {
        return new Scope(ScopeId.New(), name, protocol, realmId, description);
    }
    
    
}