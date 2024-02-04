namespace UniIdentity.Application.Tokens.Models;

/// <summary>
/// List of claims from different sources
/// https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
/// https://openid.net/specs/openid-connect-core-1_0.html#IDToken
/// </summary>
public struct UniJwtClaimNames
{
    #region JsonWebToken

    /// <summary>
    /// https://openid.net/specs/openid-connect-core-1_0.html#IDToken
    /// </summary>
    public const string Iss = "iss";
    public const string Sub = "sub";
    public const string Aud = "aud";
    public const string Exp = "exp";
    public const string Nbf = "nbf";
    public const string Iat = "iat";
    public const string Jti = "jti";
    public const string Typ = "typ";

    #endregion

    #region IdToken

    public const string Sid = "sid";
    public const string AuthTime = "auth_time";
    public const string Nonce = "nonce";
    public const string Amr = "amr";
    public const string Azp = "azp";
    public const string Name = "name";
    public const string GivenName = "given_name";
    public const string FamilyName = "family_name";
    public const string MiddleName = "middle_name";
    public const string Nickname = "nickname";
    public const string PreferredUsername = "preferred_username";
    public const string Profile = "profile";
    public const string Picture = "picture";
    public const string Website = "website";
    public const string Email = "email";
    public const string EmailVerified = "email_verified";
    public const string Gender = "gender";
    public const string Birthdate = "birthdate";
    public const string Zoneinfo = "zoneinfo";
    public const string Locale = "locale";
    public const string PhoneNumber = "phone_number";
    public const string PhoneNumberVerified = "phone_number_verified";
    public const string Address = "address";
    public const string UpdatedAt = "updated_at";

    #endregion

    #region AccessToken

    public const string Roles = "roles";
    public const string RealmAccess = "realm_access";
    public const string ResourceAccess = "resource_access";
    public const string Scope = "scope";

    #endregion

}