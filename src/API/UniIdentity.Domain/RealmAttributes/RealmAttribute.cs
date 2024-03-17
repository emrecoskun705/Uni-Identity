using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.RealmAttributes;

public class RealmAttribute : BaseEntity
{
    public RealmId Id { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }

    public Realm Realm { get; private set; }

    private RealmAttribute(
        RealmId id,
        string name,
        string value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
    
    private RealmAttribute() {}

    public static RealmAttribute Create(RealmId realmId, string name, string value)
    {
        return new RealmAttribute
        {
            Id = realmId,
            Name = name,
            Value = value
        };
    }
}