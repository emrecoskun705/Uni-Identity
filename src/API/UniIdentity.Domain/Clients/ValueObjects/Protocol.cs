namespace UniIdentity.Domain.Clients.ValueObjects;

public sealed record Protocol
{
    private Protocol(string value) => Value = value;
    public string Value { get; init; }

    public static Protocol OpenIdConnect => new("openid-connect");
    
    public static Protocol FromValue(string value) => new(value);
}