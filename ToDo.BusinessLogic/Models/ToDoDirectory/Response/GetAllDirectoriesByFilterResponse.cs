using Common.Models;

namespace ToDo.BusinessLogic.Models.ToDoDirectory.Response;

public class GetAllDirectoriesByFilterResponse: BaseResponseModel
{
    public IEnumerable<GetAllDirectoriesByFilterResponseItemModel> ToDoDirectories { get; set; }
}

public class GetAllDirectoriesByFilterResponseItemModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
