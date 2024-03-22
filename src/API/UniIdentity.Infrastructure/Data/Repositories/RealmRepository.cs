using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Realms.Repositories;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal class RealmRepository : Repository<Realm>, IGetRealmRepository, IAddRealmRepository, IRealmExistenceRepository
{
    public RealmRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
    
    public async Task<Realm?> GetByRealmId(RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Realm
            .FirstOrDefaultAsync(x => x.Id == realmId, cancellationToken: ct);
    }

    public async Task<bool> CheckAsync(RealmId realmId)
    {
        return await _db.Realm
            .AnyAsync(x => x.Id == realmId);
    }
}