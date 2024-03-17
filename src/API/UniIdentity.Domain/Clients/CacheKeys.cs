using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;

// ReSharper disable once CheckNamespace
namespace UniIdentity.Domain;

public static partial class CacheKeys
{
    public static readonly Func<ClientKey,RealmId, string> ClientByClientIdAndRealmId = 
        (clientId, realmId) => $"client-{clientId.Value}-{realmId.Value}";
    public static readonly Func<RealmId, ClientKey, string, string> ClientAttributeByClientKeyRealmIdName = 
        (realmAttribute, clientKey, name) => $"client-attributes-{realmAttribute.Value}-{clientKey.Value}-{name}";
}