namespace UniIdentity.Domain.Configs.ValueObjects;

public sealed record ProviderType
{
    private ProviderType(string value) => Value = value;
    public string Value { get; init; }

    public static ProviderType RsaKeyGen => new("rsa-key-generation");
    
    public static ProviderType FromValue(string value) => new(value);
}