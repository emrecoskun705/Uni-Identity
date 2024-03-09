namespace UniIdentity.Domain.Configs.Enums;

public static class SignatureAlg
{
    // https://datatracker.ietf.org/doc/html/rfc7518#section-3
    public const string RsaSha256 = "RS256";
    public const string HmacSha256 = "HS256";
    public const string None = "none";
}