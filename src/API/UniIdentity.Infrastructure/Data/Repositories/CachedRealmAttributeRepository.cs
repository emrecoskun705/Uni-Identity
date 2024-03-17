using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.RealmAttributes;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedRealmAttributeRepository : IGetRealmAttributeRepository
{
    private readonly IGetRealmAttributeRepository _realmRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedRealmAttributeRepository(
        [FromKeyedServices(ServiceKey.RealmOriginalKey)]IGetRealmAttributeRepository realmRepository, 
        IMemoryCache memoryCache)
    {
        _realmRepository = realmRepository;
        _memoryCache = memoryCache;
    }
    
    public async Task<RealmAttribute> GetByNameAsync(RealmId realmId, string name, CancellationToken ct = default)
    {
        return (await _memoryCache.GetOrCreateAsync(
            CacheKeys.RealmAttributeCacheKey(realmId, name),
            _ => _realmRepository.GetByNameAsync(realmId, name, ct)))!;
    }
    
}