using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Realms;
using CacheKeys = UniIdentity.Domain.Clients.CacheKeys;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedClientRepository : IGetClientRepository
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromDays(1);
    private readonly IGetClientRepository _clientRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedClientRepository(
        [FromKeyedServices(ServiceKey.ClientOriginalKey)]IGetClientRepository clientRepository, 
        IMemoryCache memoryCache)
    {
        _clientRepository = clientRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Client?> GetByClientKeyAndRealmIdAsync(ClientKey clientKey, RealmId realmId, CancellationToken ct = default)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.ClientByClientIdAndRealmId(clientKey, realmId),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);
                return _clientRepository.GetByClientKeyAndRealmIdAsync(clientKey, realmId, ct);
            });
    }
}