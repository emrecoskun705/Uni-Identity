using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients;

public static class CacheKeys
{
    public static readonly Func<ClientKey,RealmId, string> ClientByClientIdAndRealmId = 
        (clientId, realmId) => $"client-{clientId.Value}-{realmId.Value}";
    public static readonly Func<ClientId, string> ClientAttributesByClientId = 
        (clientId) => $"client-attributes-{clientId.Value}";
}