using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
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
public abstract class JsonWebToken : IToken
{
    [JsonPropertyName(UniJwtClaimNames.Iss), Token]
    public string? Issuer { get;  set; }
    
    [JsonPropertyName(UniJwtClaimNames.Sub), Token]
    public string? Subject { get; set; }
    
    
    private string[]? _audience;
    
    [JsonPropertyName(UniJwtClaimNames.Aud), Token]
    public string? Audience => string.Join(' ', _audience??[]);

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
        if (_audience == null)
        {
            _audience = new[] { audience };
        }
        else
        {
            // if audience already exists then no need to add audience
            if (_audience.Any(audItem => audience == audItem))
            {
                return this;
            }
        }
        
        Array.Resize(ref _audience, _audience.Length + 1);

        _audience[^1] = audience;
        
        return this;
    }

    public virtual TokenType GetTokenType()
    {
        return TokenType.JsonWebToken;
    }

    /// <summary>
    /// Retrieves a collection of claims based on the properties of the current object.
    /// </summary>
    /// <remarks>
    /// This method iterates over the properties of the object and generates claims based on the presence of specific attributes.
    /// If a property has both a [TokenAttribute] and a [JsonPropertyNameAttribute], its value is converted to a claim with the specified JSON property name.
    /// </remarks>
    /// <returns>
    /// A collection of claims derived from the object's properties and custom claims.
    /// </returns>
    public IEnumerable<Claim> GetClaims()
    {
        var claims = new List<Claim>();

        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<TokenAttribute>();
            var jsonPropertyName = property.GetCustomAttribute<JsonPropertyNameAttribute>();
            
            if (attribute == null || jsonPropertyName == null) continue;
                
            var value = property.GetValue(this);
            string claimValue;
            
            switch (value)
            {
                case Dictionary<string, AccessToken.Access> resourceValue:
                    claimValue = GetResourceAccessClaimValue(resourceValue);
                    claims.Add(new Claim(jsonPropertyName.Name, claimValue, JsonClaimValueTypes.Json));
                    continue;
                case AccessToken.Access accessValue:
                    claimValue = GetAccessClaimValue(accessValue);
                    claims.Add(new Claim(jsonPropertyName.Name, claimValue, JsonClaimValueTypes.Json));
                    continue;
            }

            // Convert other types to string
            claimValue = value?.ToString() ?? string.Empty;
            
            if (value != null)
            {
                claims.Add(new Claim(jsonPropertyName.Name, claimValue));
            }
        }

        foreach (var customClaim in CustomClaims)
        {
            claims.Add(new Claim(customClaim.Key, customClaim.Value.ToString() ?? string.Empty));
        }

        return claims;
    }

    /// <summary>
    /// Converts a dictionary of resource access values into a JSON string representation.
    /// </summary>
    /// <param name="resourceValue">The dictionary containing resource access values.</param>
    /// <returns>A JSON string representing the resource access values.</returns>
    private static string GetResourceAccessClaimValue(Dictionary<string, AccessToken.Access> resourceValue)
    {
        var resourceAccessObject = new Dictionary<string, object>();
        foreach (var (resourceKey, access) in resourceValue)
        {
            if (access.Roles != null)
            {
                var accessObject = new
                {
                    roles = access.Roles.ToArray()
                };
                resourceAccessObject.Add(resourceKey, accessObject);
            }
        }
        var claimValue = JsonSerializer.Serialize(resourceAccessObject, typeof(object));

        return claimValue;
    }
    
    /// <summary>
    /// Converts an access value object into a JSON string representation.
    /// </summary>
    /// <param name="accessValue">The access value object.</param>
    /// <returns>A JSON string representing the access value.</returns>
    private static string GetAccessClaimValue(AccessToken.Access accessValue)
    {
        // Serialize enumerable value to JSON
        var rolesObject = new { roles = accessValue.Roles };
        var claimValue = JsonSerializer.Serialize(rolesObject, typeof(object));
        return claimValue;
    }
}