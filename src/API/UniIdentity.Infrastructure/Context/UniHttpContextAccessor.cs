using Microsoft.AspNetCore.Http;
using UniIdentity.Application.Contracts.Context;

namespace UniIdentity.Infrastructure.Context;

internal sealed class UniHttpContextAccessor : IUniHttpContextAccessor
{
    private static readonly int RealmIndex = 2;
    private static readonly char RealmPathSplitChar = '/';
    private static readonly string PathStartsWith = "/auth/realms/";
    private static readonly string ClientIdName = "client_id";
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UniHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        RealmId = ExtractRealmIdFromUrl(HttpContext.Request.Path);
        ClientId = ExtractClientIdFromBody();
    }

    public HttpContext HttpContext => _httpContextAccessor.HttpContext!;
    
    public string? ClientId {get; init; }

    public string? RealmId { get; init; }
    
    private static string? ExtractRealmIdFromUrl(PathString path)
    {
        if (path.Value == null)
            return null;
        
        var segments = path.Value.Trim(RealmPathSplitChar).Split(RealmPathSplitChar);

        if (path.Value.StartsWith(PathStartsWith))
        {
            return segments[RealmIndex];
        }

        return null;
    }
    
    private string? ExtractClientIdFromBody()
    {
        var formData = HttpContext.Request.Form;
        if (formData.TryGetValue(ClientIdName, out var clientId))
            return clientId;

        return null;
    }
}