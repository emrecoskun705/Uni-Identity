using System.Security.Cryptography;
using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

/// <summary>
/// Represents a configuration for RSA key generation associated with a realm within the UniIdentity domain.
/// </summary>
public sealed class RsaGenerationConfig : Config
{
    // RSA Configuration attributes
    private const string PublicKeyName = "publicKey";
    private const string PrivateKeyName = "privateKey";
    private const string Algorithm = "algorithm";
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RsaGenerationConfig"/> class with the specified identifier, realm identifier, and name.
    /// </summary>
    /// <param name="id">The unique identifier of the configuration.</param>
    /// <param name="realmId">The identifier of the realm to which the configuration belongs.</param>
    /// <param name="name">The name of the configuration.</param>
    private RsaGenerationConfig(ConfigId id, RealmId realmId, string name)
        : base(id, realmId, name, ProviderType.RsaKeyGen, new List<ConfigAttribute>())
    {
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="RsaGenerationConfig"/> with the specified configurations.
    /// </summary>
    /// <param name="realmId">The identifier of the realm to which the configuration belongs.</param>
    /// <param name="name">The name of the configuration.</param>
    /// <param name="publicKey">The public key value.</param>
    /// <param name="privateKey">The private key value.</param>
    /// <param name="algorithm">The RSA algorithm.</param>
    /// <returns>A new instance of <see cref="RsaGenerationConfig"/>.</returns>
    public static RsaGenerationConfig CreateWithConfigurations(RealmId realmId, string name, string publicKey, string privateKey, string algorithm)
    {
        var config = new RsaGenerationConfig(ConfigId.New(), realmId, name)
        {
            ConfigAttributes = new List<ConfigAttribute>()
        };
        
        config.AddConfigAttribute(PublicKeyName, publicKey);
        config.AddConfigAttribute(PrivateKeyName, privateKey);
        config.AddConfigAttribute(Algorithm, algorithm);

        return config;
    }

    /// <summary>
    /// Retrieves the public key from the configuration attributes.
    /// </summary>
    /// <returns>The public key.</returns>
    public string GetPublicKey()
    {
        return ConfigAttributes.First(x => x.Name == PublicKeyName).Value;
    }

    /// <summary>
    /// Retrieves the private key from the configuration attributes.
    /// </summary>
    /// <returns>The private key.</returns>
    public string GetPrivateKey()
    {
        return ConfigAttributes.First(x => x.Name == PrivateKeyName).Value;
    }

    /// <summary>
    /// Retrieves the RSA algorithm from the configuration attributes.
    /// </summary>
    /// <returns>The RSA algorithm.</returns>
    public string GetAlgorithm()
    {
        return ConfigAttributes.First(x => x.Name == Algorithm).Value;
    }

    /// <summary>
    /// Retrieves the RSA parameters from the configuration attributes.
    /// </summary>
    /// <returns>The RSA parameters.</returns>
    public RSAParameters GetRsaParameters()
    {
        var privateKeyBytes = Convert.FromBase64String(GetPrivateKey());
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

        return rsa.ExportParameters(true);
    }
}