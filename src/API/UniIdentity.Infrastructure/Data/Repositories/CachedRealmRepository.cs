using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Realms.Repositories;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedRealmRepository : IGetRealmRepository
{
    private readonly IGetRealmRepository _realmRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedRealmRepository(
        [FromKeyedServices(ServiceKey.RealmOriginalKey)]IGetRealmRepository realmRepository, 
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
    
}