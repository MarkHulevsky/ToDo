using ToDo.BusinessLogic.Models.ToDoNote.Request;
using ToDo.BusinessLogic.Models.ToDoNote.Response;

namespace ToDo.BusinessLogic.Services.Interfaces;

public interface IToDoNoteService
{
    Task<GetAllToDoNotesByDirectoryIdResponse> GetAllByDirectoryIdAsync(Guid directoryId);

    Task CreateAsync(CreateToDoNoteRequest request);

    Task UpdateAsync(UpdateToDoNoteRequest request);
}