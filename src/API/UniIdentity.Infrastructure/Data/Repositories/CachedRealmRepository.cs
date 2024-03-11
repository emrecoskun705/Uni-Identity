using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedRealmRepository : IRealmRepository
{
    private readonly IRealmRepository _realmRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedRealmRepository(
        [FromKeyedServices(ServiceKey.RealmOriginalKey)]IRealmRepository realmRepository, 
        IMemoryCache memoryCache)
    {
        _realmRepository = realmRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Realm?> GetByRealmId(RealmId realmId, CancellationToken ct = default)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.RealmById(realmId),
            _ => _realmRepository.GetByRealmId(realmId, ct));
    }

    public async Task<RealmAttribute> GetRealmAttributeAsync(RealmId realmId, string name, CancellationToken ct = default)
    {
        return (await _memoryCache.GetOrCreateAsync(
            CacheKeys.RealmAttributeCacheKey(realmId, name),
            _ => _realmRepository.GetRealmAttributeAsync(realmId, name, ct)))!;
    }
}