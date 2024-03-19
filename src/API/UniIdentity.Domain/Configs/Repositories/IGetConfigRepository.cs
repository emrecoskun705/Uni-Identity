using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs.Repositories;

public interface IGetConfigRepository
{
    Task<RsaGenerationConfig?> GetRsaGenerationConfigAsync(RealmId realmId, string name, CancellationToken ct = default);
    Task<HmacGenerationConfig?> GetHmacGenerationConfigAsync(RealmId realmId, string name, CancellationToken ct = default);
}