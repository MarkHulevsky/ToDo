using ToDo.BusinessLogic.Services.Interfaces;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.BusinessLogic.Services;

public class ToDoNoteService: IToDoNoteService
{
    private readonly IToDoNoteRepository _toDoNoteRepository;

    public ToDoNoteService(IToDoNoteRepository toDoNoteRepository)
    {
        _toDoNoteRepository = toDoNoteRepository;
    }
}