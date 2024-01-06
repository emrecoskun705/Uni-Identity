namespace UniIdentity.Domain.Credentials.ValueObjects;

public sealed record CredentialType
{
    private CredentialType(string value) => Value = value;
    public string Value { get; init; }

    public static CredentialType Password => new("password");
    
    public static CredentialType FromValue(string value) => new(value);
}