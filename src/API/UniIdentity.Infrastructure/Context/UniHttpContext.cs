using Microsoft.AspNetCore.Http;
using UniIdentity.Application.Contracts.Context;
using UniIdentity.Domain.Clients;
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
    
    public UniHttpContext(IHttpContextAccessor httpContextAccessor, IClientRepository clientRepository, IRealmRepository realmRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _clientRepository = clientRepository;
        _realmRepository = realmRepository;
        RealmId = ExtractRealmIdFromUrl(HttpContext.Request.Path);
        ClientKey = ExtractClientIdFromBody();
    }

    public HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public ClientKey? ClientKey { get; }

    public RealmId? RealmId { get; }

    public async Task<ClientAttribute?> GetClientAttributeAsync(string attributeName)
    {
        if (ClientKey == null || RealmId == null)
            return null;
        
        var client = await _clientRepository.GetByClientIdAndRealmIdAsync(ClientKey, RealmId);

        if (client == null)
            return null;
        
        // get it from cached client attributes
        var clientAttributes = await _clientRepository.GetClientAttributesAsync(RealmId, ClientKey);

        return clientAttributes.FirstOrDefault(x => x.Name == attributeName);
    }

    public async Task<Realm?> GetRealmAsync(CancellationToken ct = default)
    {
        if (RealmId == null)
            return null;
        
        return await _realmRepository.GetByRealmId(RealmId, ct);
    }

    public async Task<Client?> GetClientAsync(CancellationToken ct = default)
    {
        if (RealmId == null || ClientKey == null)
            return null;

        return await _clientRepository.GetByClientIdAndRealmIdAsync(ClientKey, RealmId, ct);
    }

    public async Task<IEnumerable<ClientAttribute>> GetClientAttributesAsync(CancellationToken ct = default)
    {
        if (ClientKey == null || RealmId == null)
            return [];
        // get it from cached client repository by default
        return await _clientRepository.GetClientAttributesAsync(RealmId, ClientKey, ct);
    }

    public async Task<IEnumerable<RealmAttribute>> GetRealmAttributesAsync(CancellationToken ct = default)
    {
        if (RealmId == null)
            return [];
        // get it from cached realm repository by default
        return await _realmRepository.GetRealmAttributesAsync(RealmId, ct);
    }

    private static RealmId? ExtractRealmIdFromUrl(PathString path)
    {
        if (path.Value == null)
            return null;
        
        var segments = path.Value.Trim(RealmPathSplitChar).Split(RealmPathSplitChar);

        if (path.Value.StartsWith(PathStartsWith))
        {
            return new RealmId(segments[RealmIndex]);
        }

        return null;
    }
    
    private ClientKey? ExtractClientIdFromBody()
    {
        var formData = HttpContext.Request.Form;
        if (formData.TryGetValue(ClientIdName, out var clientId))
            return ClientKey.FromValue(clientId!);

        return null;
    }
}