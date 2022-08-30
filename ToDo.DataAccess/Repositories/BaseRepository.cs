using Common.Entities;
using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess.Repositories;

public abstract class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity: BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    protected DbSet<TEntity> EntitySet => _dbContext.Set<TEntity>();

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
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

    protected async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}