namespace UniIdentity.Domain.Configs.Enums;

/// <summary>
/// Provides constant values representing signature algorithms used for generating tokens.
/// </summary>
/// <remarks>
/// Signature algorithms define the cryptographic mechanisms used to secure tokens. This static class contains constant strings representing commonly used signature algorithms, including RSA and HMAC variants.
/// </remarks>
public static class SignatureAlg
{
    // https://datatracker.ietf.org/doc/html/rfc7518#section-3
    /// <summary>
    /// Represents the RSA-SHA256 signature algorithm.
    /// </summary>
    public const string RsaSha256 = "RS256";

    /// <summary>
    /// Represents the HMAC-SHA256 signature algorithm.
    /// </summary>
    public const string HmacSha256 = "HS256";

    /// <summary>
    /// Represents the default signature algorithm (RSA-SHA256).
    /// </summary>
    public const string Default = "RS256";

    /// <summary>
    /// Represents the "none" signature algorithm indicating no cryptographic protection.
    /// </summary>
    public const string None = "none";
}