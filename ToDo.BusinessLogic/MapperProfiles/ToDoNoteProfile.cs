using AutoMapper;
using ToDo.BusinessLogic.Models.ToDoDirectory.Response;
using ToDo.DataAccess.Entities;

namespace ToDo.BusinessLogic.MapperProfiles;

public class ToDoNoteProfile: Profile
{
    public ToDoNoteProfile()
    {
        CreateMap<ToDoNote, GetAllUserDirectoriesToDoNoteResponseModel>();
    }
}