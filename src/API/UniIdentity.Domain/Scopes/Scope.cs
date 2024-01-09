using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Scopes;

public sealed class Scope : BaseEntity<ScopeId>
{
    public string Name { get; private set; }
    public string Protocol { get; private set; }
    public RealmId RealmId { get; private set; }
    public string Description { get; private set; }

    public Realm Realm { get; private set; }

    public Scope(ScopeId id, string name, string protocol, RealmId realmId, string description)
        : base(id)
    {
        Name = name;
        Protocol = protocol;
        RealmId = realmId;
        RealmId = realmId;
        Description = description;
    }
}