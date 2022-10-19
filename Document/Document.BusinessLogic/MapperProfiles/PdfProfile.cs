using AutoMapper;
using Document.BusinessLogic.HttpClients.Models.ToDo.Response;
using Document.BusinessLogic.Models;

namespace Document.BusinessLogic.MapperProfiles;

public class PdfProfile: Profile
{
    public PdfProfile()
    {
        CreateMap<GetToDoDirectoryByIdResponse, ToDoDirectoryPdfModel>();

        CreateMap<GetToDoDirectoryByIdNoteResponseItemModel, ToDoDirectoryPdfNoteModel>();
    }
}