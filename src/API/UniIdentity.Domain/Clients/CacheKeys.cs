using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients;

public static class CacheKeys
{
    public static readonly Func<ClientKey,RealmId, string> ClientByClientIdAndRealmId = 
        (clientId, realmId) => $"client-{clientId.Value}-{realmId.Value}";
    public static readonly Func<RealmId, ClientKey, string, string> ClientAttributeByClientKeyRealmIdName = 
        (realmAttribute, clientKey, name) => $"client-attributes-{realmAttribute.Value}-{clientKey.Value}-{name}";
}