using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess.Repositories;

public class ToDoNoteRepository: BaseRepository<ToDoNote>, IToDoNoteRepository
{
    public ToDoNoteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<ToDoNote>> GetAllByDirectoryIdAsync(Guid directoryId)
    {
        return await EntitySet
            .Where(note => note.ToDoDirectoryId == directoryId)
            .ToListAsync();
    }
}