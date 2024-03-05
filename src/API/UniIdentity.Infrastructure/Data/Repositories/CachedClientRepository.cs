using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;
using CacheKeys = UniIdentity.Domain.Clients.CacheKeys;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedClientRepository : IClientRepository
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromDays(1);
    private readonly IClientRepository _clientRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedClientRepository(
        [FromKeyedServices(ServiceKey.ClientOriginalKey)]IClientRepository clientRepository, 
        IMemoryCache memoryCache)
    {
        _clientRepository = clientRepository;
        _memoryCache = memoryCache;
    }
    
    public async Task<Client?> GetByClientIdAndRealmIdAsync(ClientKey clientKey, RealmId realmId, CancellationToken ct = default)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.ClientByClientIdAndRealmId(clientKey, realmId),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);
                return _clientRepository.GetByClientIdAndRealmIdAsync(clientKey, realmId, ct);
            });
    }

    
    public async Task<IEnumerable<ClientAttribute>> GetClientAttributesAsync(RealmId realmId, ClientKey clientKey, CancellationToken ct = default)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.ClientByClientIdAndRealmId(clientKey, realmId),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);
                return _clientRepository.GetClientAttributesAsync(realmId, clientKey, ct);
            }) ?? [];
    }
}