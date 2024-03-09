﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Domain;
using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Realms;
using CacheKeys = UniIdentity.Domain.Configs.CacheKeys;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class CachedConfigRepository : IConfigRepository
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromDays(30);
    private readonly IConfigRepository _configRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedConfigRepository(
        [FromKeyedServices(ServiceKey.ConfigOriginalKey)]IConfigRepository configRepository, 
        IMemoryCache memoryCache)
    {
        _configRepository = configRepository;
        _memoryCache = memoryCache;
    }

    public async Task<RsaGenerationConfig?> GetRsaGenerationConfigAsync(RealmId realmId, string name, CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.RsaConfigByRealmIdAndName(realmId, name),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);
                return _configRepository.GetRsaGenerationConfigAsync(realmId, name, cancellationToken);
            });
    }

    public async Task<HmacGenerationConfig?> GetHmacGenerationConfigAsync(RealmId realmId, string name, CancellationToken ct = default)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.HmacConfigByRealmIdAndName(realmId, name),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);
                return _configRepository.GetHmacGenerationConfigAsync(realmId, name, ct);
            });
    }
}