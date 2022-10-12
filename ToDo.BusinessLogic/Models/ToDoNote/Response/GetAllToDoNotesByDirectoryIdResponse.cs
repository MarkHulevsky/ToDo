using Common.Models;

namespace ToDo.BusinessLogic.Models.ToDoNote.Response;

public class GetAllToDoNotesByDirectoryIdResponse: BaseResponseModel
{
    public IEnumerable<GetAllToDoNotesByDirectoryIdResponseItemModel> ToDoNotes { get; set; }
}

public class GetAllToDoNotesByDirectoryIdResponseItemModel
{
    public Guid Id { get; set; }

    public bool IsCompleted { get; set; }

    public string Note { get; set; }
}