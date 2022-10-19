using Document.BusinessLogic.Models.Response;

namespace Document.BusinessLogic.Services.Interfaces;

public interface IDocumentService
{
    Task<GeneratePdfByDirectoryIdResponse> GenerateByDirectoryIdAsync(Guid directoryId);

    Task<Stream> DownloadAsync(Guid fileId);

    Task SendViaEmailAsync(Guid fileId);
}