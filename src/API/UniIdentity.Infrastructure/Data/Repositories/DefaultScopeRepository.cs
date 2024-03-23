using UniIdentity.Domain.Scopes;
using UniIdentity.Domain.Scopes.Repositories;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class DefaultScopeRepository : Repository<DefaultScope>, IAddDefaultScopeRepository
{
    public DefaultScopeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}