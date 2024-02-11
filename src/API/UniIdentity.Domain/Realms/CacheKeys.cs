namespace UniIdentity.Domain.Realms;

public static class CacheKeys
{
    public static Func<RealmId, string> RealmById = realmId => $"realm-{realmId.Value}";
}