using System.Text.Json.Serialization;

namespace UniIdentity.Application.Tokens.Models;

/// <summary>
/// List of claims from different sources
/// https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
/// https://openid.net/specs/openid-connect-core-1_0.html#IDToken
/// https://datatracker.ietf.org/doc/html/rfc7519#section-4.1
/// </summary>
[Serializable]
public class IdToken : JsonWebToken
{
    /// <summary>
    /// https://openid.net/specs/openid-connect-frontchannel-1_0.html#RPLogout
    /// </summary>
    [JsonPropertyName(UniJwtClaimNames.Sid)]
    public string? SessionId { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.AuthTime)]
    public long? AuthTime { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Nonce)]
    public string? Nonce { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Amr)]
    public string? Amr { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Azp)]
    public string? Azp { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Name)]
    public string? Name { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.GivenName)]
    public string? GivenName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.FamilyName)]
    public string? FamilyName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.MiddleName)]
    public string? MiddleName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Nickname)]
    public string? Nickname { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PreferredUsername)]
    public string? PreferredUsername { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Profile)]
    public string? Profile { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Picture)]
    public string? Picture { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Website)]
    public string? Website { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Email)]
    public string? Email { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.EmailVerified)]
    public bool? EmailVerified { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Gender)]
    public string? Gender { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Birthdate)]
    public long? Birthdate { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Zoneinfo)]
    public string? ZoneInfo { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Locale)]
    public string? Locale { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PhoneNumber)]
    public string? PhoneNumber { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PhoneNumberVerified)]
    public bool? PhoneNumberVerified { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Address)]
    public string? Address { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.UpdatedAt)]
    public long? UpdatedAt { get; set; }
    
    public override TokenType GetTokenType()
    {
        return TokenType.Id;
    }
    
}