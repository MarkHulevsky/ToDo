using Common.Models;

namespace ToDo.BusinessLogic.Models.ToDoDirectory.Response;

public class GetToDoDirectoryByIdResponse: BaseResponseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}