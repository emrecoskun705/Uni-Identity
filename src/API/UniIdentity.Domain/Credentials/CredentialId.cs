namespace UniIdentity.Domain.Credentials;

public record CredentialId(Guid Value)
{
    public static CredentialId FromValue(Guid value) => new CredentialId(value);

    public static CredentialId New() => FromValue(Guid.NewGuid());
}