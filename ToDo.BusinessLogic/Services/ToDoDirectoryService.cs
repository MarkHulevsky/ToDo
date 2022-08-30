using Common.Extensions;
using Common.Models;
using Microsoft.AspNetCore.Http;
using ToDo.BusinessLogic.Constants;
using ToDo.BusinessLogic.Models.ToDoDirectory.Request;
using ToDo.BusinessLogic.Services.Interfaces;
using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.BusinessLogic.Services;

public class ToDoDirectoryService: IToDoDirectoryService
{
    private readonly IToDoDirectoryRepository _toDoDirectoryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ToDoDirectoryService(IToDoDirectoryRepository toDoDirectoryRepository, IHttpContextAccessor httpContextAccessor)
    {
        _toDoDirectoryRepository = toDoDirectoryRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponseModel> CreateAsync(CreateToDoDirectoryRequest request)
    {
        Guid? userId = _httpContextAccessor.HttpContext.User.Identity.GetUserId();

        if (!userId.HasValue)
        {
            return new BaseResponseModel
            {
                Message = ToDoDirectoryConstants.USER_ID_CLAIM_MISSING
            };
        }

        var directory = new ToDoDirectory
        {
            Name = request.Name,
            UserId = userId.Value
        };

        await _toDoDirectoryRepository.CreateAsync(directory);

        return new BaseResponseModel
        {
            IsSucceeded = true
        };
    }
}