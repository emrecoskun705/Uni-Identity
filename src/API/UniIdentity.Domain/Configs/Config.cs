using UniIdentity.Domain.Common;
using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

public abstract class Config : BaseEntity
{
    public ConfigId Id { get; private set; }
    public RealmId RealmId { get; private set; }
    public string Name { get; private set; }
    public ProviderType ProviderType { get; init; }
    
    public Realm Realm { get; private set; }
    
    public ICollection<ConfigAttribute> ConfigAttributes { get; protected set; }

    protected Config(ConfigId id, RealmId realmId, string name)
    {
        Id = id;
        RealmId = realmId;
        Name = name;
    }
}