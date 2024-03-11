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

    public async Task<RealmAttribute> GetRealmAttributeAsync(RealmId realmId, string name, CancellationToken ct = default)
    {
        return await _db.RealmAttribute
            .Where(x => x.Realm.Id == realmId && x.Name == name)
            .FirstAsync(cancellationToken: ct);
    }
}