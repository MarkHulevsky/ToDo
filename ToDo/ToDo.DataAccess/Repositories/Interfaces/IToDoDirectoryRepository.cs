using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Models.ToDoDirectory;

namespace ToDo.DataAccess.Repositories.Interfaces;

public interface IToDoDirectoryRepository: IBaseRepository<ToDoDirectory>
{
    Task<IList<ToDoDirectory>> GetAllUserDirectoriesByFilterAsync(GetAllUserDirectoriesByFilterModel filterModel);
}