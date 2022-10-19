using Common.Entities;
using Document.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Document.DataAccess.Repositories;

public abstract class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity: BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    protected DbSet<TEntity> EntitySet => _dbContext.Set<TEntity>();

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await EntitySet.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task CreateAsync(TEntity entity)
    {
        await EntitySet.AddAsync(entity);

        await SaveChangesAsync();
    }

    public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        await EntitySet.AddRangeAsync(entities);

        await SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        EntitySet.Update(entity);

        await SaveChangesAsync();
    }

    protected async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}