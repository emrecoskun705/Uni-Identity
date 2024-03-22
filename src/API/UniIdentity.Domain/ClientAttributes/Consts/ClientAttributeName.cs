// ReSharper disable InconsistentNaming
namespace UniIdentity.Domain.ClientAttributes.Consts;

/// <summary>
/// Provides constant values representing attribute names for client configurations within the UniIdentity domain.
/// </summary>
public static class ClientAttributeName
{
    /// <summary>
    /// Represents the attribute name for enabling refresh tokens for a client.
    /// </summary>
    public const string EnableRefreshToken = "enable.refresh.token";
    
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
}