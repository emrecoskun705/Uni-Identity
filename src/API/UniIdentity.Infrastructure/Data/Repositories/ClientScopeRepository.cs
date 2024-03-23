using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.ClientScopes;
using UniIdentity.Domain.ClientScopes.Repositories;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class ClientScopeRepository : Repository<ClientScope>, IGetClientScopeRepository, IAddClientScopeRepository
{
    public ClientScopeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<ClientScope>> GetByClientId(ClientId clientId)
    {
        return await _db.Set<ClientScope>()
            .Where(x => x.ClientId == clientId)
            .ToListAsync();
    }
}