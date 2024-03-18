using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.RealmAttributes;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class RealmAttributeRepository : Repository<RealmAttribute>, IGetRealmAttributeRepository, IAddRealmAttributeRepository
{
    public RealmAttributeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<RealmAttribute> GetByNameAsync(RealmId realmId, string name, CancellationToken ct = default)
    {
        return await _db.RealmAttribute
            .Where(x => x.Id == realmId && x.Name == name)
            .FirstAsync(cancellationToken: ct);
    }
    
}