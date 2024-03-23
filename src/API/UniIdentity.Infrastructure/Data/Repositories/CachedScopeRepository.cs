using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Scopes;
using UniIdentity.Domain.Scopes.Repositories;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedScopeRepository : IGetScopeRepository
{
    private readonly IGetScopeRepository _getScopeRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedScopeRepository(
        [FromKeyedServices(ServiceKey.ScopeOriginalKey)]IGetScopeRepository getScopeRepository, 
        IMemoryCache memoryCache)
    {
        _getScopeRepository = getScopeRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IReadOnlyCollection<Scope>> GetAllAsync(RealmId realmId, CancellationToken ct = default)
    {
        return (await _memoryCache.GetOrCreateAsync(
            CacheKeys.ScopesByRealmId(realmId),
            _ => _getScopeRepository.GetAllAsync(realmId, ct)))!;
    }
}