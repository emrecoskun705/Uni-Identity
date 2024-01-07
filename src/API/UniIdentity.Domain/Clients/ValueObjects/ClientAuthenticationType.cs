namespace UniIdentity.Domain.Clients.ValueObjects;

public sealed record ClientAuthenticationType
{
    private ClientAuthenticationType(string value) => Value = value;
    public string Value { get; init; }

    public static ClientAuthenticationType ClientSecret => new("client-secret");
    
    public static ClientAuthenticationType FromValue(string value) => new(value);
}