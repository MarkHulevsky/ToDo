using ToDo.DataAccess.Entities;

namespace ToDo.DataAccess.Repositories.Interfaces;

public interface IToDoNoteRepository: IBaseRepository<ToDoNote>
{
    Task<IList<ToDoNote>> GetAllByDirectoryIdAsync(Guid directoryId);
}