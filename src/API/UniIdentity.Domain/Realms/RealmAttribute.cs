using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Realms;

public class RealmAttribute : BaseEntity<RealmId>
{
    public string Name { get; private set; }
    public string Value { get; private set; }

    public Realm Realm { get; private set; }

    public RealmAttribute(
        RealmId id,
        string name,
        string value)
        : base(id)
    {
        Name = name;
        Value = value;
    }
}