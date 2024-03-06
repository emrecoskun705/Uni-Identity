using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Realms;

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

    public static RealmAttribute Create(string name, string value)
    {
        return new RealmAttribute
        {
            Name = name,
            Value = value
        };
    }
}