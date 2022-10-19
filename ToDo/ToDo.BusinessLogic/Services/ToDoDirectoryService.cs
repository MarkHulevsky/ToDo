using AutoMapper;
using Common.Extensions;
using Common.Models;
using Microsoft.AspNetCore.Http;
using ToDo.BusinessLogic.Constants;
using ToDo.BusinessLogic.Models.ToDoDirectory.Request;
using ToDo.BusinessLogic.Models.ToDoDirectory.Response;
using ToDo.BusinessLogic.Services.Interfaces;
using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Models.ToDoDirectory;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.BusinessLogic.Services;

public class ToDoDirectoryService: IToDoDirectoryService
{
    private readonly IToDoDirectoryRepository _toDoDirectoryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public ToDoDirectoryService(IToDoDirectoryRepository toDoDirectoryRepository,
        IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _toDoDirectoryRepository = toDoDirectoryRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<GetAllDirectoriesByFilterResponse> GetAllUserDirectoriesAsync(GetAllDirectoriesByFilterRequest request)
    {
        Guid? userId = _httpContextAccessor.HttpContext.User.Identity!.GetUserId()!;

        var filterModel = _mapper.Map<GetAllUserDirectoriesByFilterModel>(request);

        filterModel.UserId = userId.Value;

        IList<ToDoDirectory> toDoDirectories =
            await _toDoDirectoryRepository.GetAllUserDirectoriesByFilterAsync(filterModel);

        var response = new GetAllDirectoriesByFilterResponse
        {
            ToDoDirectories = _mapper.Map<IEnumerable<GetAllDirectoriesByFilterResponseItemModel>>(toDoDirectories)
        };

        return response;
    }

    public async Task<GetToDoDirectoryByIdResponse> GetByIdAsync(Guid id)
    {
        ToDoDirectory? toDoDirectory = await _toDoDirectoryRepository.GetByIdAsync(id);

        var response = _mapper.Map<GetToDoDirectoryByIdResponse>(toDoDirectory);

        return response;
    }

    public async Task<BaseResponseModel> CreateAsync(CreateToDoDirectoryRequest request)
    {
        Guid? userId = _httpContextAccessor.HttpContext.User.Identity!.GetUserId();

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