using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients;

public static class CacheKeys
{
    public static readonly Func<ClientId,RealmId, string> ClientByClientIdAndRealmId = (clientId, realmId) => $"client-{clientId.Value}-{realmId.Value}";
    public static readonly Func<ClientUniqueId, string> ClientAttributeByClientId = (clientId) => $"client-attribute-{clientId.Value}";
}