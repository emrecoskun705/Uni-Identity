using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class ClientRepository : Repository<Client>, IGetClientRepository, IAddClientRepository, IClientExistenceRepository
{
    public ClientRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
    
    public async Task<Client?> GetByClientIdAndRealmIdAsync(ClientKey clientKey, RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Client
            .FirstOrDefaultAsync(x => x.ClientKey == clientKey && x.RealmId == realmId, ct);
    }
    
    public async Task<Client?> GetByClientKeyAndRealmIdAsync(ClientKey clientKey, RealmId realmId, CancellationToken ct = default)
    {
        return await _db.Client
            .FirstOrDefaultAsync(x => x.ClientKey == clientKey && x.RealmId == realmId, ct);
    }

    public async Task<bool> CheckAsync(ClientId clientId)
    {
        return await _db.Client
            .AnyAsync(x => x.Id == clientId);
    }

    public async Task<bool> CheckAsync(RealmId realmId, ClientKey clientKey)
    {
        return await _db.Client
            .AnyAsync(x => x.RealmId == realmId && x.ClientKey == clientKey);
    }
}