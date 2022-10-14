using Document.BusinessLogic.Models.Response;

namespace Document.BusinessLogic.Services.Interfaces;

public interface IPdfService
{
    Task<GeneratePdfByDirectoryIdResponse> GenerateByDirectoryIdAsync(Guid directoryId);

    Task<Stream> DownloadAsync(Guid fileId);
}