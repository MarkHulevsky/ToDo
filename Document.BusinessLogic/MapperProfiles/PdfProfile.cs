using AutoMapper;
using Pdf.BusinessLogic.HttpClients.Models.ToDo.Response;
using Pdf.BusinessLogic.Models;

namespace Pdf.BusinessLogic.MapperProfiles;

public class PdfProfile: Profile
{
    public PdfProfile()
    {
        CreateMap<GetToDoDirectoryByIdResponse, ToDoDirectoryPdfModel>();

        CreateMap<GetToDoDirectoryByIdNoteResponseItemModel, ToDoDirectoryPdfNoteModel>();
    }
}