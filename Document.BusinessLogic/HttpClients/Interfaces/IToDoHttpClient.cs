using Document.BusinessLogic.HttpClients.Models.ToDo.Response;

namespace Document.BusinessLogic.HttpClients.Interfaces;

public interface IToDoHttpClient
{
    Task<GetToDoDirectoryByIdResponse?> GetToDoDirectoryByIdAsync(Guid directoryId);
}