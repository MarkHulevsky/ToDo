using AutoMapper;
using ToDo.BusinessLogic.Models.ToDoDirectory.Response;
using ToDo.DataAccess.Entities;

namespace ToDo.BusinessLogic.MapperProfiles;

public class DirectoryProfile: Profile
{
    public DirectoryProfile()
    {
        CreateMap<ToDoDirectory, GetAllUserDirectoriesResponse>();
        // .ForMember(dest => dest.Message, options => options.Ignore())
        // .ForMember(dest => dest.IsSucceeded, options => options.Ignore());
    }
}