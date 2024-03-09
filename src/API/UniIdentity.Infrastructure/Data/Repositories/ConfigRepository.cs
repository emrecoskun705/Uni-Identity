using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class ConfigRepository : Repository<Config>, IConfigRepository
{
    public ConfigRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<RsaGenerationConfig?> GetRsaGenerationConfigAsync(RealmId realmId, string name, CancellationToken cancellationToken)
    {
        return await _db.Config
            .OfType<RsaGenerationConfig>()
            .Include(config => config.ConfigAttributes)
            .Where(
                x =>
                    x.ProviderType == ProviderType.RsaKeyGen &&
                    x.RealmId == realmId &&
                    x.Name == name)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}