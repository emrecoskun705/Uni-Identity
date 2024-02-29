using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

public class Config : BaseEntity
{
    public ConfigId Id { get; private set; }
    public RealmId RealmId { get; private set; }
    public string Name { get; private set; }
    public string ProviderId { get; private set; }
    
    public Realm Realm { get; private set; }
    
    public ICollection<ConfigAttribute> ConfigAttributes { get; private set; }

    public Config(ConfigId id, RealmId realmId, string name, string providerId)
    {
        Id = id;
        RealmId = realmId;
        Name = name;
        ProviderId = providerId;
    }
}