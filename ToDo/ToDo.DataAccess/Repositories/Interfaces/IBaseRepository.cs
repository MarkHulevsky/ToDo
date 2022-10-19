using Common.Entities;

namespace ToDo.DataAccess.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity: BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task CreateAsync(TEntity entity);

    Task CreateRangeAsync(IEnumerable<TEntity> entities);

    Task UpdateAsync(TEntity entity);
}