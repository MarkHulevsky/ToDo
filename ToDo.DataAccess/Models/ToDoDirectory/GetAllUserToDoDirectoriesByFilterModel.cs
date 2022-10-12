namespace ToDo.DataAccess.Models.ToDoDirectory;

public class GetAllUserDirectoriesByFilterModel
{
    public Guid UserId { get; set; }

    public string? SearchText { get; set; }
}