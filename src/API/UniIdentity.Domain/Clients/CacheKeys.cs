using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients;

public static class CacheKeys
{
    public static readonly Func<string,RealmId, string> ClientByClientIdAndRealmId = (clientId, realmId) => $"client-{clientId}-{realmId.Value}";
}