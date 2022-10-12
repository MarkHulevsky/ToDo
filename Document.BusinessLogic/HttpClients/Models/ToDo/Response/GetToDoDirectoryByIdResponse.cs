using Common.Models;

namespace Pdf.BusinessLogic.HttpClients.Models.ToDo.Response;

public class GetToDoDirectoryByIdResponse: BaseResponseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<GetToDoDirectoryByIdNoteResponseItemModel> ToDoNotes { get; set; }
}

public class GetToDoDirectoryByIdNoteResponseItemModel
{
    public Guid Id { get; set; }

    public string Note { get; set; }

    public bool IsCompleted { get; set; }
}
