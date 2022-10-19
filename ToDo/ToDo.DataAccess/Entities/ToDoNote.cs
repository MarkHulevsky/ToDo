using Common.Entities;

namespace ToDo.DataAccess.Entities;

public class ToDoNote: BaseEntity
{
    public bool IsCompleted { get; set; }

    public string Note { get; set; }

    public Guid ToDoDirectoryId { get; set; }
}