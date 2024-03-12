using Microsoft.AspNetCore.Http;
using UniIdentity.Application.Contracts.Context;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Context;

internal sealed class UniHttpContext : IUniHttpContext
{
    private static readonly int RealmIndex = 2;
    private static readonly char RealmPathSplitChar = '/';
    private static readonly string PathStartsWith = "/auth/realms/";
    private static readonly string ClientIdName = "client_id";
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IClientRepository _clientRepository;
    private readonly IRealmRepository _realmRepository;
    private readonly IConfigRepository _configRepository;
    
    public UniHttpContext(IHttpContextAccessor httpContextAccessor, IClientRepository clientRepository, IRealmRepository realmRepository, IConfigRepository configRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _clientRepository = clientRepository;
        _realmRepository = realmRepository;
        _configRepository = configRepository;
        RealmId = ExtractRealmIdFromUrl(HttpContext.Request.Path);
        ClientKey = ExtractClientIdFromBody();
    }

    public HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public ClientKey? ClientKey { get; }

    public RealmId RealmId { get; }

    public async Task<ClientAttribute?> GetClientAttributeAsync(string attributeName)
    {
        if (ClientKey == null)
            return null;
        
        var client = await _clientRepository.GetByClientIdAndRealmIdAsync(ClientKey, RealmId);

        if (client == null)
            return null;
        
        // get it from cached client attributes
        var clientAttributes = await _clientRepository.GetClientAttributesAsync(RealmId, ClientKey);

        return clientAttributes.FirstOrDefault(x => x.Name == attributeName);
    }

    public async Task<Realm> GetRealmAsync(CancellationToken ct = default)
    {
        if (RealmId == null)
            throw new ArgumentNullException(nameof(RealmId), "RealmId cannot be null.");
        
        return await _realmRepository.GetByRealmId(RealmId, ct)
            ?? throw new InvalidOperationException("Failed to retrieve Realm with the provided RealmId.");;
    }

    public async Task<Client> GetClientAsync(CancellationToken ct = default)
    {
        if (ClientKey == null)
            throw new ArgumentNullException(nameof(ClientKey), "ClientKey cannot be null.");

        return await _clientRepository.GetByClientIdAndRealmIdAsync(ClientKey, RealmId, ct)
            ?? throw new InvalidOperationException("Failed to retrieve Client with the provided ClientKey and RealmId.");
    }
    
    public async Task<RsaGenerationConfig> GetRsaGenerationConfigAsync(string name, CancellationToken ct = default)
    {
        return await _configRepository.GetRsaGenerationConfigAsync(RealmId, name, ct)
            ?? throw new InvalidOperationException("Failed to retrieve RsaGenerationConfig with the provided RealmId and name.");
    }

    public async Task<HmacGenerationConfig> GetHmacGenerationConfigAsync(string name, CancellationToken ct = default)
    {
        return await _configRepository.GetHmacGenerationConfigAsync(RealmId, name, ct)
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