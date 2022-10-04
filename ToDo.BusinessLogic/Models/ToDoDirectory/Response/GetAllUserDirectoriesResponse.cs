using Common.Models;

namespace ToDo.BusinessLogic.Models.ToDoDirectory.Response;

public class GetAllUserDirectoriesResponse: BaseResponseModel
{
    public IEnumerable<GetAllUserDirectoriesDirectoryResponseModel> ToDoDirectories { get; set; }
}

public class GetAllUserDirectoriesDirectoryResponseModel
{
    public string Name { get; set; }

    public IEnumerable<GetAllUserDirectoriesToDoNoteResponseModel> ToDoNotes { get; set; }
}

public class GetAllUserDirectoriesToDoNoteResponseModel
{
    public bool IsCompleted { get; set; }

    public string Note { get; set; }
}