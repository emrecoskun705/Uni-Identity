namespace UniIdentity.Domain.Configs.ValueObjects;

/// <summary>
/// Represents a provider type used for key generation operations.
/// </summary>
/// <remarks>
/// Provider types define the mechanism or algorithm used for generating cryptographic keys. Instances of this record represent specific provider types used within the system.
/// </remarks>
public sealed record ProviderType
{
    /// <summary>
    /// Gets the value of the provider type.
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ProviderType"/> record with the specified value.
    /// </summary>
    /// <param name="value">The value of the provider type.</param>
    private ProviderType(string value) => Value = value;
    
    /// <summary>
    /// Represents the provider type for RSA key generation.
    /// </summary>
    public static ProviderType RsaKeyGen => new("rsa-key-generation");
    
    /// <summary>
    /// Represents the provider type for HMAC key generation.
    /// </summary>
    public static ProviderType HmacKeyGen => new("hmac-key-generation");
    
    public static ProviderType FromValue(string value) => new(value);
}