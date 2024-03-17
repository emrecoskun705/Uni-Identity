namespace UniIdentity.Domain.OIDC;

// ReSharper disable once InconsistentNaming
/// <summary>
/// Provides constant values representing attribute names commonly used in OpenID Connect (OIDC) scenarios.
/// </summary>
public static class OIDCAttribute
{
    /// <summary>
    /// Represents the attribute name for the access token algorithm in OIDC.
    /// </summary>
    public const string AccessTokenAlgorithm = "access.token.algorithm";
    
    /// <summary>
    /// Represents the attribute name for the ID token algorithm in OIDC.
    /// </summary>
    public const string IdTokenAlgorithm = "id.token.algorithm";
}