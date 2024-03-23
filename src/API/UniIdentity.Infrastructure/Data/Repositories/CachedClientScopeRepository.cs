using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.ClientScopes;
using UniIdentity.Domain.ClientScopes.Repositories;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedClientScopeRepository : IGetClientScopeRepository
{
    private readonly IGetClientScopeRepository _getClientScopeRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedClientScopeRepository(
        [FromKeyedServices(ServiceKey.ClientScopeOriginalKey)]IGetClientScopeRepository getClientScopeRepository, 
        IMemoryCache memoryCache)
    {
        _getClientScopeRepository = getClientScopeRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IReadOnlyCollection<ClientScope>> GetByClientId(ClientId clientId)
    {
        return (await _memoryCache.GetOrCreateAsync(
                   CacheKeys.ClientScopesByClientId,
                   _ => _getClientScopeRepository.GetByClientId(clientId)))!;
    }
}