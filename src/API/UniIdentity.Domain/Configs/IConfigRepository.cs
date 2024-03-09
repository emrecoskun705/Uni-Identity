using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

public interface IConfigRepository
{
    Task<RsaGenerationConfig?> GetRsaGenerationConfigAsync(RealmId realmId, string name, CancellationToken ct = default);
    Task<HmacGenerationConfig?> GetHmacGenerationConfigAsync(RealmId realmId, string name, CancellationToken ct = default);
}