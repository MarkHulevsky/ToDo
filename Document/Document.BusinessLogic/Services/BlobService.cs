using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Document.BusinessLogic.Constants;
using Document.BusinessLogic.Services.Interfaces;

namespace Document.BusinessLogic.Services;

public class BlobService: IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<Stream> DownloadAsync(string name)
    {
        BlobContainerClient? container = _blobServiceClient.GetBlobContainerClient(AzureBlobStorageConstants.CONTAINER_NAME);

        BlobClient blobClient = container.GetBlobClient(name);

        Response<BlobDownloadInfo> file = await blobClient.DownloadAsync();

        return file.Value.Content;
    }

    public async Task<string> UploadAsync(Stream file)
    {
        BlobContainerClient? container = _blobServiceClient.GetBlobContainerClient(AzureBlobStorageConstants.CONTAINER_NAME);

        string fileName = string.Format(AzureBlobStorageConstants.TODO_FILE_NAME_TEMPLATE, DateTime.UtcNow.Ticks);

        await container.UploadBlobAsync(fileName, file);

        return fileName;
    }
}