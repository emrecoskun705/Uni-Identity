using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
    
    public async Task<Client?> GetByClientIdAndRealmId(ClientId clientId, RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Client.FirstOrDefaultAsync(x => x.ClientId == clientId && x.RealmId == realmId, ct);
    }
}