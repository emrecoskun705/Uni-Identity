using UniIdentity.Domain.Realms;

// ReSharper disable once CheckNamespace
namespace UniIdentity.Domain;

public static partial  class CacheKeys
{
    public static readonly Func<RealmId, string, string> RealmAttributeCacheKey = (realmId, name) => $"realm-attribute-{realmId.Value}-{name}";
}