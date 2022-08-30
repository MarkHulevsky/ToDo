using Common.Entities;

namespace ToDo.DataAccess.Entities;

public class ToDoDirectory: BaseEntity
{
    public string Name { get; set; }

    public Guid UserId { get; set; }

    public IEnumerable<ToDoNote> ToDoNotes { get; set; }
}