using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class ClientRepository : Repository<Client>, IGetClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
    
    public async Task<Client?> GetByClientIdAndRealmIdAsync(ClientKey clientKey, RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Client.FirstOrDefaultAsync(x => x.ClientKey == clientKey && x.RealmId == realmId, ct);
    }
    

    public async Task<Client?> GetByClientKeyAndRealmIdAsync(ClientKey clientKey, RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Client.FirstOrDefaultAsync(x => x.ClientKey == clientKey && x.RealmId == realmId, ct);
    }
}