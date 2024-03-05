using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

public static class CacheKeys
{
    public static readonly Func<RealmId, string, string> ConfigsByRealmIdAndName = (realmId, name) => $"config-{realmId.Value}-{name}";
}