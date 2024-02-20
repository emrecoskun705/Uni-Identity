using System.Text.Json.Serialization;
using UniIdentity.Application.Tokens.Attributes;
using UniIdentity.Common.Json;

namespace UniIdentity.Application.Tokens.Models;

/// <summary>
/// List of claims from different sources
/// https://openid.net/specs/openid-connect-core-1_0.html#IDToken
/// https://datatracker.ietf.org/doc/html/rfc7519#section-4.1
/// </summary>
[Serializable]
public class JsonWebToken : IToken
{
    [JsonPropertyName(UniJwtClaimNames.Iss), Token]
    public string? Issuer { get;  set; }
    
    [JsonPropertyName(UniJwtClaimNames.Sub), Token]
    public string? Subject { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Aud), Token]
    [JsonConverter(typeof(TokenStringArrayConverter))]
    private string[]? _audience;
    
    public string[]? Audience {
        get => _audience;
        private set => _audience = value;
    }
    
    [JsonPropertyName(UniJwtClaimNames.Exp), Token]
    public long? Expiration { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Nbf), Token]
    public long? NotBefore { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Iat), Token]
    public long? IssuedAt { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Jti), Token]
    public string? JwtId { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Typ), Token]
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