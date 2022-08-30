using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess.Repositories;

public class ToDoNoteRepository: BaseRepository<ToDoNote>, IToDoNoteRepository
{
    public ToDoNoteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}