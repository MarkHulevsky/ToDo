using AutoMapper;
using ToDo.BusinessLogic.Models.ToDoNote.Response;
using ToDo.DataAccess.Entities;

namespace ToDo.BusinessLogic.MapperProfiles;

public class ToDoNoteProfile: Profile
{
    public ToDoNoteProfile()
    {
        CreateMap<ToDoNote, GetAllToDoNotesByDirectoryIdResponseItemModel>();
    }
}