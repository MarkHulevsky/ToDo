using AutoMapper;
using ToDo.BusinessLogic.Models.ToDoDirectory.Request;
using ToDo.BusinessLogic.Models.ToDoDirectory.Response;
using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Models.ToDoDirectory;

namespace ToDo.BusinessLogic.MapperProfiles;

public class ToDoDirectoryProfile : Profile
{
    public ToDoDirectoryProfile()
    {
        CreateMap<GetAllDirectoriesByFilterRequest, GetAllUserDirectoriesByFilterModel>()
            .ForMember(dest => dest.UserId, options => options.Ignore());

        CreateMap<ToDoDirectory, GetAllDirectoriesByFilterResponseItemModel>();

        CreateMap<ToDoDirectory, GetToDoDirectoryByIdResponse>();

        CreateMap<ToDoNote, GetToDoDirectoryByIdNoteResponseItemModel>();
    }
}