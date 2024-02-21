using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal class RealmRepository : Repository<Realm>, IRealmRepository
{
    public RealmRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
    
    public async Task<Realm?> GetByRealmId(RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Realm.FirstOrDefaultAsync(x => x.Id == realmId, cancellationToken: ct);
    }

    public async Task<IEnumerable<RealmAttribute>> GetRealmAttributesAsync(RealmId realmId, CancellationToken ct = default)
    { 
        return await _db.Realm
            .Join(_db.RealmAttribute, 
                realm => realm, 
                realmAttribute => realmAttribute.Realm,
                (realm, realmAttribute) => realmAttribute)
            .Where(x => x.Realm.Id == realmId)
            .ToListAsync(cancellationToken: ct);
    }
}