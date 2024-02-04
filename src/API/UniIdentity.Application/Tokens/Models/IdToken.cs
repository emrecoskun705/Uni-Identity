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
    protected long? AuthTime { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Nonce)]
    protected string? Nonce { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Amr)]
    protected string? Amr { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Azp)]
    protected string? Azp { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Name)]
    protected string? Name { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.GivenName)]
    protected string? GivenName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.FamilyName)]
    protected string? FamilyName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.MiddleName)]
    protected string? MiddleName { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Nickname)]
    protected string? Nickname { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PreferredUsername)]
    protected string? PreferredUsername { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Profile)]
    protected string? Profile { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Picture)]
    protected string? Picture { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Website)]
    protected string? Website { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Email)]
    protected string? Email { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.EmailVerified)]
    protected bool? EmailVerified { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Gender)]
    protected string? Gender { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Birthdate)]
    protected long? Birthdate { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Zoneinfo)]
    protected string? ZoneInfo { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Locale)]
    protected string? Locale { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PhoneNumber)]
    protected string? PhoneNumber { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.PhoneNumberVerified)]
    protected bool? PhoneNumberVerified { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Address)]
    protected string? Address { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.UpdatedAt)]
    protected long? UpdatedAt { get; set; }
    
    public override TokenType GetTokenType()
    {
        return TokenType.Id;
    }
    
}