using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess.Repositories;

public class ToDoDirectoryRepository: BaseRepository<ToDoDirectory>, IToDoDirectoryRepository
{
    public ToDoDirectoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}