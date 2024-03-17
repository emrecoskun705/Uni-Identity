using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.ClientAttributes;
using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedClientAttributeRepository : IGetClientAttributeRepository
{
    private readonly IGetClientAttributeRepository _getClientAttributeRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedClientAttributeRepository(
        [FromKeyedServices(ServiceKey.ClientOriginalKey)]IGetClientAttributeRepository getClientAttributeRepository, 
        IMemoryCache memoryCache)
    {
        _getClientAttributeRepository = getClientAttributeRepository;
        _memoryCache = memoryCache;
    }
    
    public async Task<ClientAttribute> GetByNameAsync(RealmId realmId, ClientKey clientKey, string name, CancellationToken ct = default)
    {
        return (await _memoryCache.GetOrCreateAsync(
                CacheKeys.ClientAttributeByClientKeyRealmIdName(realmId, clientKey, name),
                _ => _getClientAttributeRepository.GetByNameAsync(realmId, clientKey, name, ct)))!;
    }
}