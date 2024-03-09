using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

public sealed class RsaGenerationConfig : Config
{
    // RSA Configuration attributes
    private const string PublicKeyName = "publicKey";
    private const string PrivateKeyName = "privateKey";
    private const string Algorithm = "algorithm";
    
    private RsaGenerationConfig(ConfigId id, RealmId realmId, string name)
        : base(id, realmId, name)
    {
        ProviderType = ProviderType.RsaKeyGen;
    }
    
    public static RsaGenerationConfig CreateWithConfigurations(RealmId realmId, string name, string publicKey, string privateKey, string algorithm)
    {
        var config = new RsaGenerationConfig(ConfigId.New(), realmId, name)
        {
            ConfigAttributes = new List<ConfigAttribute>()
        };
        config.ConfigAttributes.Add(new ConfigAttribute(config.Id, PublicKeyName, publicKey));
        config.ConfigAttributes.Add(new ConfigAttribute(config.Id, PrivateKeyName, privateKey));
        config.ConfigAttributes.Add(new ConfigAttribute(config.Id, Algorithm, algorithm));
        return config;
    }

    public string GetPublicKey()
    {
        return ConfigAttributes.First(x => x.Name == PublicKeyName).Value;
    }

    public string GetPrivateKey()
    {
        return ConfigAttributes.First(x => x.Name == PrivateKeyName).Value;
    }

    public string GetAlgorithm()
    {
        return ConfigAttributes.First(x => x.Name == Algorithm).Value;
    }
}