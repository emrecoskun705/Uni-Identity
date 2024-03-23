using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Scopes;
using UniIdentity.Domain.Scopes.Repositories;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class ScopeRepository : Repository<Scope>, IAddScopeRepository, IGetScopeRepository
{
    public ScopeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Scope>> GetAllAsync(RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Scope
            .Where(x => x.RealmId == realmId)
            .ToListAsync(ct);
    }
}