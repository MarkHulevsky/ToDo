namespace ToDo.BusinessLogic.Models.ToDoNote.Request;

public class UpdateToDoNoteRequest
{
    public Guid Id { get; set; }

    public bool IsCompleted { get; set; }

    public string Note { get; set; }
}