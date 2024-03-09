using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

public sealed class HmacGenerationConfig : Config
{
    private const string Secret = "secret";
    private const string Algorithm = "algorithm";
    private const string KeyId = "kid";
    
    
    public HmacGenerationConfig(ConfigId id, RealmId realmId, string name)
        : base(id, realmId, name)
    {
        ProviderType = ProviderType.HmacKeyGen;
    }

    public static HmacGenerationConfig CreateWithConfigurations(
        RealmId realmId, string name, string algorithm, string secret, string keyId)
    {
        var config = new HmacGenerationConfig(ConfigId.New(), realmId, name)
        {
            ConfigAttributes = new List<ConfigAttribute>()
        };
        config.ConfigAttributes.Add(new ConfigAttribute(config.Id, Secret, secret));
        config.ConfigAttributes.Add(new ConfigAttribute(config.Id, Algorithm, algorithm));
        config.ConfigAttributes.Add(new ConfigAttribute(config.Id, KeyId, keyId));
        return config;
    }

    public string GetSecret()
    {
        return ConfigAttributes.First(x => x.Name == Secret).Value;
    }
    
    public string GetAlgorithm()
    {
        return ConfigAttributes.First(x => x.Name == Algorithm).Value;
    }
    
    public string GetKeyId()
    {
        return ConfigAttributes.First(x => x.Name == KeyId).Value;
    }
}