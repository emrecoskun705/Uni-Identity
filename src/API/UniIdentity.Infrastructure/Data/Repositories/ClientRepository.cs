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
    
    public async Task<Client?> GetByClientIdAndRealmIdAsync(ClientKey clientKey, RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Client.FirstOrDefaultAsync(x => x.ClientKey == clientKey && x.RealmId == realmId, ct);
    }
    
    public async Task<IEnumerable<ClientAttribute>> GetClientAttributesAsync(RealmId realmId, ClientKey clientKey, CancellationToken ct = default)
    {
        return await _db.Client
            .Join(_db.ClientAttribute, 
                client => client, 
                clientAttribute => clientAttribute.Client,
                (client, clientAttribute) => clientAttribute)
            .Where(x => x.Client.RealmId == realmId && x.Client.ClientKey == clientKey)
            .ToListAsync(cancellationToken: ct);
    }
}