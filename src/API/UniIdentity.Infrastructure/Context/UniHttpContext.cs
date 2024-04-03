using Microsoft.AspNetCore.Http;
using UniIdentity.Application.Contracts.Context;
using UniIdentity.Domain.ClientAttributes;
using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Configs.Repositories;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Realms.Repositories;

namespace UniIdentity.Infrastructure.Context;

internal sealed class UniHttpContext : IUniHttpContext
{
    private static readonly int RealmIndex = 2;
    private static readonly char RealmPathSplitChar = '/';
    private static readonly string PathStartsWith = "/auth/realms/";
    private static readonly string ClientIdName = "client_id";
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGetClientRepository _getClientRepository;
    private readonly IGetClientAttributeRepository _getClientAttributeRepository;
    private readonly IGetRealmRepository _getRealmRepository;
    private readonly IGetConfigRepository _getConfigRepository;
    
    public UniHttpContext(
        IHttpContextAccessor httpContextAccessor, 
        IGetClientRepository getClientRepository, 
        IGetRealmRepository getRealmRepository, 
        IGetConfigRepository getConfigRepository, 
        IGetClientAttributeRepository getClientAttributeRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _getClientRepository = getClientRepository;
        _getRealmRepository = getRealmRepository;
        _getConfigRepository = getConfigRepository;
        _getClientAttributeRepository = getClientAttributeRepository;
        RealmId = ExtractRealmIdFromUrl(HttpContext.Request.Path);
        ClientKey = ExtractClientIdFromBody();
    }
    
    /// <inheritdoc />
    public HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    /// <inheritdoc />
    public ClientKey? ClientKey { get; }

    /// <inheritdoc />
    public RealmId RealmId { get; }

    /// <inheritdoc />
    public async Task<ClientAttribute?> GetClientAttributeAsync(string attributeName)
    {
        if (ClientKey == null)
            return null;

        return await _getClientAttributeRepository.GetByNameAsync(RealmId, ClientKey, attributeName);
    }

    /// <inheritdoc />
    public async Task<Realm> GetRealmAsync(CancellationToken ct = default)
    {
        if (RealmId == null)
            throw new ArgumentNullException(nameof(RealmId), "RealmId cannot be null.");
        
        return await _getRealmRepository.GetByRealmId(RealmId, ct)
            ?? throw new InvalidOperationException("Failed to retrieve Realm with the provided RealmId.");
    }
    
    /// <inheritdoc />
    public async Task<Client> GetClientAsync(CancellationToken ct = default)
    {
        if (ClientKey == null)
            throw new ArgumentNullException(nameof(ClientKey), "ClientKey cannot be null.");

        return await _getClientRepository.GetByClientKeyAndRealmIdAsync(ClientKey, RealmId, ct)
            ?? throw new InvalidOperationException("Failed to retrieve Client with the provided ClientKey and RealmId.");
    }
    
    /// <inheritdoc />
    public async Task<RsaGenerationConfig> GetRsaGenerationConfigAsync(string name, CancellationToken ct = default)
    {
        return await _getConfigRepository.GetRsaGenerationConfigAsync(RealmId, name, ct)
            ?? throw new InvalidOperationException("Failed to retrieve RsaGenerationConfig with the provided RealmId and name.");
    }

    /// <inheritdoc />
    public async Task<HmacGenerationConfig> GetHmacGenerationConfigAsync(string name, CancellationToken ct = default)
    {
        return await _getConfigRepository.GetHmacGenerationConfigAsync(RealmId, name, ct)
            ?? throw new InvalidOperationException("Failed to retrieve HmacGenerationConfig with the provided RealmId and name.");
    }

    private static RealmId ExtractRealmIdFromUrl(PathString path)
    {
        if (path.Value == null)
            throw new ArgumentNullException(nameof(path));
        
        var segments = path.Value.Trim(RealmPathSplitChar).Split(RealmPathSplitChar);

        if (path.Value.StartsWith(PathStartsWith))
        {
            return new RealmId(segments[RealmIndex]);
        }

        throw new InvalidOperationException("Invalid path format");
    }
    
    private ClientKey? ExtractClientIdFromBody()
    {
        var formData = HttpContext.Request.Form;
        if (formData.TryGetValue(ClientIdName, out var clientId))
            return ClientKey.FromValue(clientId!);

        return null;
    }
}