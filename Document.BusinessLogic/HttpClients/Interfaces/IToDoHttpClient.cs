using Pdf.BusinessLogic.HttpClients.Models.ToDo.Response;

namespace Pdf.BusinessLogic.HttpClients.Interfaces;

public interface IToDoHttpClient
{
    Task<GetToDoDirectoryByIdResponse?> GetToDoDirectoryByIdAsync(Guid directoryId);
}