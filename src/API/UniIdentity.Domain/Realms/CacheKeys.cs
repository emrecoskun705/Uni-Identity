namespace UniIdentity.Domain.Realms;

public static class CacheKeys
{
    public static readonly Func<RealmId, string> RealmById = realmId => $"realm-{realmId.Value}";
    public static readonly Func<RealmId, string, string> RealmAttributeCacheKey = (realmId, name) => $"realm-attribute-{realmId.Value}-{name}";
}