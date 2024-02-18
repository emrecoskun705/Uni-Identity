using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.ValueObjects;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedClientAttributeRepository : IClientAttributeRepository
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromDays(1);
    private readonly IClientAttributeRepository _clientAttributeRepository;
    private readonly IMemoryCache _memoryCache;


    public CachedClientAttributeRepository([FromKeyedServices("og")]IClientAttributeRepository clientAttributeRepository, IMemoryCache memoryCache)
    {
        _clientAttributeRepository = clientAttributeRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<ClientAttribute>?> GetClientAttributes(ClientUniqueId clientUniqueId)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.ClientAttributeByClientId(clientUniqueId),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);
                return _clientAttributeRepository.GetClientAttributes(clientUniqueId);
            });
    }
}