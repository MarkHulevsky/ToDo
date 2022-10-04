using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess.Repositories;

public class ToDoDirectoryRepository: BaseRepository<ToDoDirectory>, IToDoDirectoryRepository
{
    public ToDoDirectoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<ToDoDirectory>> GetAllUserDirectoriesAsync(Guid userId)
    {
       return await EntitySet
            .Where(directory => directory.UserId == userId)
            .Include(directory => directory.ToDoNotes)
            .ToListAsync();
    }
}