using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

/// <summary>
/// Represents a configuration for HMAC key generation associated with a realm within the UniIdentity domain.
/// </summary>
public sealed class HmacGenerationConfig : Config
{
    private const string Secret = "secret";
    private const string Algorithm = "algorithm";
    private const string KeyId = "kid";
    
    /// <summary>
    /// Initializes a new instance of the <see cref="HmacGenerationConfig"/> class with the specified identifier, realm identifier, and name.
    /// </summary>
    /// <param name="id">The unique identifier of the configuration.</param>
    /// <param name="realmId">The identifier of the realm to which the configuration belongs.</param>
    /// <param name="name">The name of the configuration.</param>
    public HmacGenerationConfig(ConfigId id, RealmId realmId, string name)
        : base(id, realmId, name, ProviderType.HmacKeyGen, new List<ConfigAttribute>())
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="HmacGenerationConfig"/> with the specified configurations.
    /// </summary>
    /// <param name="realmId">The identifier of the realm to which the configuration belongs.</param>
    /// <param name="name">The name of the configuration.</param>
    /// <param name="algorithm">The HMAC algorithm.</param>
    /// <param name="secret">The secret key.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>A new instance of <see cref="HmacGenerationConfig"/>.</returns>
    public static HmacGenerationConfig CreateWithConfigurations(
        RealmId realmId, string name, string algorithm, string secret, string keyId)
    {
        var config = new HmacGenerationConfig(ConfigId.New(), realmId, name)
        {
            ConfigAttributes = new List<ConfigAttribute>()
        };
        
        config.AddConfigAttribute(Secret, secret);
        config.AddConfigAttribute(Algorithm, algorithm);
        config.AddConfigAttribute(KeyId, keyId);
        
        return config;
    }

    /// <summary>
    /// Retrieves the secret key from the configuration attributes.
    /// </summary>
    /// <returns>The secret key.</returns>
    public string GetSecret()
    {
        return ConfigAttributes.First(x => x.Name == Secret).Value;
    }
    
    /// <summary>
    /// Retrieves the HMAC algorithm from the configuration attributes.
    /// </summary>
    /// <returns>The HMAC algorithm.</returns>
    public string GetAlgorithm()
    {
        return ConfigAttributes.First(x => x.Name == Algorithm).Value;
    }
    
    /// <summary>
    /// Retrieves the key identifier from the configuration attributes.
    /// </summary>
    /// <returns>The key identifier.</returns>
    public string GetKeyId()
    {
        return ConfigAttributes.First(x => x.Name == KeyId).Value;
    }
}