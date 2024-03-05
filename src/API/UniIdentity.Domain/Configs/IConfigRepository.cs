using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

public interface IConfigRepository
{
    Task<RsaGenerationConfig?> GetRsaGenerationConfigAsync(RealmId realmId, string name);
}