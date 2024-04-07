using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Scopes;
using UniIdentity.Domain.Scopes.Repositories;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class DefaultScopeRepository : Repository<DefaultScope>, IAddDefaultScopeRepository, IGetDefaultScopeRepository
{
    public DefaultScopeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<DefaultScope>> GetAllByRealmId(RealmId realmId, CancellationToken cancellationToken = default)
    {
        return await _db.Set<DefaultScope>()
            .Where(x => x.RealmId == realmId)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}