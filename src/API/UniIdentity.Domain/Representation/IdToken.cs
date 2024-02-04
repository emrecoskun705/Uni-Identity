using System.Text.Json.Serialization;

namespace UniIdentity.Domain.Representation;

/// <summary>
/// List of below claims from different sources
/// https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
/// https://openid.net/specs/openid-connect-core-1_0.html#IDToken
/// </summary>
[Serializable]
public class IdToken : JsonWebToken
{
    /// <summary>
    /// https://openid.net/specs/openid-connect-frontchannel-1_0.html#RPLogout
    /// </summary>
    [JsonPropertyName("sid")]
    public string? SessionId { get; set; }
    
    [JsonPropertyName("auth_time")]
    protected long? AuthTime { get; set; }
    
    [JsonPropertyName("nonce")]
    protected string? Nonce { get; set; }
    
    [JsonPropertyName("amr")]
    protected string? Amr { get; set; }
    
    [JsonPropertyName("azp")]
    protected string? Azp { get; set; }
    
    [JsonPropertyName("name")]
    protected string? Name { get; set; }
    
    [JsonPropertyName("given_name")]
    protected string? GivenName { get; set; }
    
    [JsonPropertyName("family_name")]
    protected string? FamilyName { get; set; }
    
    [JsonPropertyName("middle_name")]
    protected string? MiddleName { get; set; }
    
    [JsonPropertyName("nickname")]
    protected string? Nickname { get; set; }
    
    [JsonPropertyName("preferred_username")]
    protected string? PreferredUsername { get; set; }
    
    [JsonPropertyName("profile")]
    protected string? Profile { get; set; }
    
    [JsonPropertyName("picture")]
    protected string? Picture { get; set; }
    
    [JsonPropertyName("website")]
    protected string? Website { get; set; }
    
    [JsonPropertyName("email")]
    protected string? Email { get; set; }
    
    [JsonPropertyName("email_verified")]
    protected bool? EmailVerified { get; set; }
    
    [JsonPropertyName("gender")]
    protected string? Gender { get; set; }
    
    [JsonPropertyName("birthdate")]
    protected long? Birthdate { get; set; }
    
    [JsonPropertyName("zoneinfo")]
    protected string? ZoneInfo { get; set; }
    
    [JsonPropertyName("locale")]
    protected string? Locale { get; set; }
    
    [JsonPropertyName("phone_number")]
    protected string? PhoneNumber { get; set; }
    
    [JsonPropertyName("phone_number_verified")]
    protected bool? PhoneNumberVerified { get; set; }
    
    [JsonPropertyName("address")]
    protected string? Address { get; set; }
    
    [JsonPropertyName("updated_at")]
    protected long? UpdatedAt { get; set; }
    
    public override TokenType GetTokenType()
    {
        return TokenType.Id;
    }
    
}