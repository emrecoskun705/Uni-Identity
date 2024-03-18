using UniIdentity.Domain.Common;

namespace UniIdentity.Infrastructure.Data.Repositories;

public abstract class Repository<TEntity> 
    where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _db;

    public Repository(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }
    
    public async Task AddAsync(TEntity entity)
    {
        await _db.AddAsync(entity);
    }
    
    public void Add(TEntity entity)
    {
        _db.Add(entity);
    }
}