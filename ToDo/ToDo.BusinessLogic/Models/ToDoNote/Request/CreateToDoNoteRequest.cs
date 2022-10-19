namespace ToDo.BusinessLogic.Models.ToDoNote.Request;

public class CreateToDoNoteRequest
{
    public Guid ToDoDirectoryId { get; set; }

    public string Note { get; set; }
}