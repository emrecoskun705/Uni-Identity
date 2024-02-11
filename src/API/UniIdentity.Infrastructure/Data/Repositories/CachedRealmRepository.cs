using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedRealmRepository : IRealmRepository
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromDays(1);
    private readonly IRealmRepository _realmRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedRealmRepository([FromKeyedServices("og")]IRealmRepository realmRepository, IMemoryCache memoryCache)
    {
        _realmRepository = realmRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Realm?> GetByRealmId(RealmId realmId, CancellationToken ct = default)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.RealmById(realmId),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);
                return _realmRepository.GetByRealmId(realmId, ct);
            });
    }
}