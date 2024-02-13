using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Clients;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal sealed class ClientAttributeRepository : Repository<ClientAttribute>, IClientAttributeRepository
{
    public ClientAttributeRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<ClientAttribute>?> GetClientAttributes(ClientId clientId)
    {
        return await _db.ClientAttribute
            .Where(x => x.Id == clientId)
            .ToListAsync();
    }
}