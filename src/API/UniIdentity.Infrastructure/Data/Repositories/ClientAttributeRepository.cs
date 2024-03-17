using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.ClientAttributes;
using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Repositories;

public class ClientAttributeRepository : Repository<ClientAttribute>, IGetClientAttributeRepository
{
    public ClientAttributeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<ClientAttribute> GetByNameAsync(RealmId realmId, ClientKey clientKey, string name, CancellationToken ct = default)
    {
        return await _db.Client
            .Join(_db.ClientAttribute,
                client => client.Id,
                clientAttribute => clientAttribute.Id,
                (client, clientAttribute) => new { client, clientAttribute })
            .Where(x => x.client.RealmId == realmId && x.client.ClientKey == clientKey && x.clientAttribute.Name == name)
            .Select(x => x.clientAttribute)
            .FirstAsync(ct);
    }
}