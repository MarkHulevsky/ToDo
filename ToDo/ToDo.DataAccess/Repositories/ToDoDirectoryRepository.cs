using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Models.ToDoDirectory;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess.Repositories;

public class ToDoDirectoryRepository : BaseRepository<ToDoDirectory>, IToDoDirectoryRepository
{
    public ToDoDirectoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override Task<ToDoDirectory?> GetByIdAsync(Guid id)
    {
        return EntitySet
            .Include(directory => directory.ToDoNotes)
            .FirstOrDefaultAsync(directory => directory.Id == id);
    }

    public async Task<IList<ToDoDirectory>> GetAllUserDirectoriesByFilterAsync(
        GetAllUserDirectoriesByFilterModel filterModel)
    {
        var searchTextPattern = $"%{filterModel.SearchText?.Trim()}%";

        return await EntitySet
            .Where(directory => directory.UserId == filterModel.UserId
                                && EF.Functions.Like(directory.Name, searchTextPattern)
            )
            .ToListAsync();
    }
}