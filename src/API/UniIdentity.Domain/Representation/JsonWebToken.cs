using System.Text.Json.Serialization;
using UniIdentity.Common.Json;

namespace UniIdentity.Domain.Representation;

/// <summary>
/// List of below claims from different sources
/// https://openid.net/specs/openid-connect-core-1_0.html#IDToken
/// https://datatracker.ietf.org/doc/html/rfc7519#section-4.1
/// </summary>
[Serializable]
public class JsonWebToken : IToken
{
    [JsonPropertyName("iss")]
    public string? Issuer { get;  set; }
    
    [JsonPropertyName("sub")]
    public string? Subject { get; set; }
    
    [JsonPropertyName("aud")]
    [JsonConverter(typeof(TokenStringArrayConverter))]
    private string[]? _audience;
    
    public string[]? Audience {
        get => _audience;
        private set => _audience = value;
    }
    
    [JsonPropertyName("exp")]
    public long? Expiration { get; set; }
    
    [JsonPropertyName("nbf")]
    public long? NotBefore { get; set; }
    
    [JsonPropertyName("iat")]
    public long? IssuedAt { get; set; }
    
    [JsonPropertyName("jti")]
    public string? JwtId { get; set; }
    
    [JsonPropertyName("typ")]
    public string? Type { get; set; }
    
    public Dictionary<string, object> CustomClaims { get; } = new();

    public void AddNewClaim(string name, object value)
    {
        CustomClaims.Add(name, value);
    }

    public JsonWebToken AddAudience(string audience)
    {
        if (Audience == null)
        {
            Audience = new[] { audience };
        }
        else
        {
            // if audience already exists then no need to add audience
            if (Audience.Any(audItem => audience == audItem))
            {
                return this;
            }
        }
        
        Array.Resize(ref _audience, Audience.Length + 1);

        Audience[^1] = audience;
        
        return this;
    }

    public virtual TokenType GetTokenType()
    {
        return TokenType.JsonWebToken;
    }
}