using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using UniIdentity.Domain.Configs.Enums;
using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

/// <summary>
/// Represents a configuration for RSA key generation associated with a realm within the UniIdentity domain.
/// </summary>
public sealed class RsaGenerationConfig : Config
{
    // RSA Configuration attributes
    private const string PrivateKeyName = "privateKey";
    private const string KeyUseName = "keyUse";
    private const string PriorityName = "priority";
    private const int DefaultKeySize = 2048;
    private const int DefaultPriority = 100;

    /// <summary>
    /// The cached value of the public key retrieved from the configuration attributes.
    /// </summary>
    [NotMapped]
    private string? _publicKey;
    
    /// <summary>
    /// The cached value of the private key retrieved from the configuration attributes.
    /// </summary>
    [NotMapped]
    private string? _privateKey;
    
    /// <summary>
    /// The cached value of the key size retrieved from the configuration attributes.
    /// </summary>
    [NotMapped]
    private int? _keySize;
    
    /// <summary>
    /// The cached value of the priority retrieved from the configuration attributes.
    /// </summary>
    [NotMapped]
    private int? _priortiy;
    
    
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
    /// <param name="keySize">The key size of RSA</param>
    /// <param name="priority">The priority of configuration</param>
    /// <returns>A new instance of <see cref="RsaGenerationConfig"/>.</returns>
    public static RsaGenerationConfig CreateWithConfigurations(RealmId realmId, string name, int keySize = DefaultKeySize, int priority = DefaultPriority)
    {
        var config = new RsaGenerationConfig(ConfigId.New(), realmId, name)
        {
            ConfigAttributes = new List<ConfigAttribute>()
        };

        var privateKeyStr = CreateRsaPrivateKey(keySize);
        
        config.AddConfigAttribute(PrivateKeyName, privateKeyStr);
        config.AddConfigAttribute(KeyUseName, KeyUse.Sig.Value);
        config.AddConfigAttribute(PriorityName, priority.ToString());

        return config;
    }

    private static string CreateRsaPrivateKey(int keySize)
    {
        using var rsa = RSA.Create(keySize);

        var privateKey = rsa.ExportRSAPrivateKey();
        var privateKeyStr = Convert.ToBase64String(privateKey);

        return privateKeyStr;
    }
    
    /// <summary>
    /// Retrieves the public key from the configuration attributes.
    /// </summary>
    /// <returns>The public key.</returns>
    public string GetPublicKey()
    {
        if (_publicKey != null) return _publicKey;
        
        var privateKeyBytes = Convert.FromBase64String(GetPrivateKey());
        
        using var rsa = RSA.Create();
        
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

        var rsaParams = rsa.ExportParameters(true);

        using var rsaPublic = RSA.Create(GetKeySize());
        
        rsaPublic.ImportParameters(rsaParams);

        // Export the public key
        var publicKeyBytes = rsaPublic.ExportRSAPublicKey();

        // Convert the public key to base64 string
        var publicKeyBase64 = Convert.ToBase64String(publicKeyBytes);

        _publicKey = publicKeyBase64;
        return _publicKey;
    }

    /// <summary>
    /// Retrieves the private key from the configuration attributes.
    /// </summary>
    /// <returns>The private key.</returns>
    public string GetPrivateKey()
    {
        if (_privateKey != null) return _privateKey;
        
        _privateKey = ConfigAttributes.First(x => x.Name == PrivateKeyName).Value;
        return _privateKey;
    }
    
    /// <summary>
    /// Retrieves the priority from the configuration attributes.
    /// </summary>
    /// <returns>The priority value.</returns>
    public int GetPriority()
    {
        if (_priortiy != null) return _priortiy.Value;
        _priortiy = Convert.ToInt32(ConfigAttributes.First(x => x.Name == PriorityName).Value);
        return _priortiy.Value;
    }
    
    /// <summary>
    /// Retrieves the key size from the configuration attributes.
    /// </summary>
    /// <returns>The key size value.</returns>
    public int GetKeySize()
    {
        if (_keySize != null) return _keySize.Value;
        _keySize = Convert.ToInt32(ConfigAttributes.First(x => x.Name == PriorityName).Value);
        return _keySize.Value;
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