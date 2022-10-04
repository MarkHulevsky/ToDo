using Common.Models;
using ToDo.BusinessLogic.Models.ToDoDirectory.Request;
using ToDo.BusinessLogic.Models.ToDoDirectory.Response;

namespace ToDo.BusinessLogic.Services.Interfaces;

public interface IToDoDirectoryService
{
    Task<BaseResponseModel> CreateAsync(CreateToDoDirectoryRequest request);
    Task<GetAllUserDirectoriesResponse> GetAllUserDirectoriesAsync();
}