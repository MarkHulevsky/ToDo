using ToDo.DataAccess.Entities;

namespace ToDo.DataAccess.Repositories.Interfaces;

public interface IToDoDirectoryRepository: IBaseRepository<ToDoDirectory>
{
    Task<IList<ToDoDirectory>> GetAllUserDirectoriesAsync(Guid userId);
}