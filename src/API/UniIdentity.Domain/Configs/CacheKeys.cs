using UniIdentity.Domain.Realms;

// ReSharper disable once CheckNamespace
namespace UniIdentity.Domain;

public static partial class CacheKeys
{
    public static readonly Func<RealmId, string, string> RsaConfigByRealmIdAndName = (realmId, name) => $"rsa-config-{realmId.Value}-{name}";
    public static readonly Func<RealmId, string, string> HmacConfigByRealmIdAndName = (realmId, name) => $"hmac-config-{realmId.Value}-{name}";
}