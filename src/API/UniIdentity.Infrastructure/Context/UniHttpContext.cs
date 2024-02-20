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
    
    public UniHttpContext(IHttpContextAccessor httpContextAccessor, IClientRepository clientRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _clientRepository = clientRepository;
        RealmId = ExtractRealmIdFromUrl(HttpContext.Request.Path);
        ClientId = ExtractClientIdFromBody();
    }

    public HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public ClientKey? ClientId { get; }

    public RealmId? RealmId { get; }

    public async Task<ClientAttribute?> GetClientAttributeAsync(string attributeName)
    {
        if (ClientId == null || RealmId == null)
            return null;
        
        var client = await _clientRepository.GetByClientIdAndRealmIdAsync(ClientId, RealmId);

        if (client == null)
            return null;
        
        // get it from cached client attributes
        var clientAttributes = await _clientRepository.GetClientAttributesAsync(RealmId, ClientId);

        return clientAttributes.FirstOrDefault(x => x.Name == attributeName);
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