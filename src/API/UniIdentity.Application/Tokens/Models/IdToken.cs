using System.Text.Json.Serialization;
using UniIdentity.Application.Tokens.Attributes;

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
    [JsonPropertyName(UniJwtClaimNames.Sid), Token]
    public string? SessionId { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.AuthTime), Token]
    public long? AuthTime { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Nonce), Token]
    public string? Nonce { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Amr), Token]
    public string? Amr { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Azp), Token]
    public string? Azp { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Name), Token]
    public string? Name { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.GivenName), Token]
    public string? GivenName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.FamilyName), Token]
    public string? FamilyName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.MiddleName), Token]
    public string? MiddleName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Nickname), Token]
    public string? Nickname { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PreferredUsername), Token]
    public string? PreferredUsername { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Profile), Token]
    public string? Profile { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Picture), Token]
    public string? Picture { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Website), Token]
    public string? Website { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Email), Token]
    public string? Email { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.EmailVerified), Token]
    public bool? EmailVerified { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Gender), Token]
    public string? Gender { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Birthdate), Token]
    public long? Birthdate { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Zoneinfo), Token]
    public string? ZoneInfo { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Locale), Token]
    public string? Locale { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PhoneNumber), Token]
    public string? PhoneNumber { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PhoneNumberVerified), Token]
    public bool? PhoneNumberVerified { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Address), Token]
    public string? Address { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.UpdatedAt), Token]
    public long? UpdatedAt { get; set; }
    
    public override TokenType GetTokenType()
    {
        return TokenType.Id;
    }
    
}