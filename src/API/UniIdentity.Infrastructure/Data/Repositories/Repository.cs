using UniIdentity.Domain.Common;

namespace UniIdentity.Infrastructure.Data.Repositories;

internal abstract class Repository<TEntity> 
    where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _db;

    public Repository(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }
    
    public void Add(TEntity entity)
    {
        _db.Add(entity);
    }
}