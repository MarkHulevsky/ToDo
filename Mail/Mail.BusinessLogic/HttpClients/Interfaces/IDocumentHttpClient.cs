namespace Mail.BusinessLogic.HttpClients.Interfaces;

public interface IDocumentHttpClient
{
    Task<Stream> DownloadFileAsync(Guid fileId);
}