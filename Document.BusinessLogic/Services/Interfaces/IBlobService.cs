namespace Document.BusinessLogic.Services.Interfaces;

public interface IBlobService
{
    Task<Stream> DownloadAsync(string name);

    Task<string> UploadAsync(Stream file);
}