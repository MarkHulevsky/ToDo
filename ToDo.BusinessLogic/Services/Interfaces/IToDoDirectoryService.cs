using Common.Models;
using ToDo.BusinessLogic.Models.ToDoDirectory.Request;

namespace ToDo.BusinessLogic.Services.Interfaces;

public interface IToDoDirectoryService
{
    Task<BaseResponseModel> CreateAsync(CreateToDoDirectoryRequest request);
}