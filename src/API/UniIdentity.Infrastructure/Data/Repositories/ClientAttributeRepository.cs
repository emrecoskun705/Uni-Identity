using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.ValueObjects;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class ClientAttributeRepository : Repository<ClientAttribute>, IClientAttributeRepository
{
    public ClientAttributeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<ClientAttribute>?> GetClientAttributes(ClientUniqueId clientUniqueId)
    {
        return await _db.ClientAttribute
            .Where(x => x.UniqueId == clientUniqueId)
            .ToListAsync();
    }
}