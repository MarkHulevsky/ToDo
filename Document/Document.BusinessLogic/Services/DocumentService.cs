using System.Text;
using AutoMapper;
using Common.Extensions;
using Document.BusinessLogic.Helpers.Interfaces;
using Document.DataAccess.Entities;
using Document.DataAccess.Repositories.Interfaces;
using Document.BusinessLogic.HttpClients.Interfaces;
using Document.BusinessLogic.HttpClients.Models.ToDo.Response;
using Document.BusinessLogic.MessageQueueClients.Interfaces;
using Document.BusinessLogic.Models;
using Document.BusinessLogic.Models.Request;
using Document.BusinessLogic.Models.Response;
using Document.BusinessLogic.Providers.Interfaces;
using Document.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Document.BusinessLogic.Services;

public class DocumentService: IDocumentService
{
    private readonly IToDoHttpClient _httpClient;
    private readonly IRazorProvider _razorProvider;
    private readonly IPdfHelper _pdfHelper;
    private readonly IMapper _mapper;
    private readonly IBlobService _blobService;
    private readonly IBlobFileRepository _blobFileRepository;
    private readonly IMessageQueueClient _messageQueueClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DocumentService(
        IToDoHttpClient httpClient,
        IPdfHelper pdfHelper, IRazorProvider razorProvider,
        IMapper mapper,
        IBlobService blobService,
        IBlobFileRepository blobFileRepository,
        IMessageQueueClient messageQueueClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _pdfHelper = pdfHelper;
        _razorProvider = razorProvider;
        _mapper = mapper;
        _blobService = blobService;
        _blobFileRepository = blobFileRepository;
        _messageQueueClient = messageQueueClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GeneratePdfByDirectoryIdResponse> GenerateByDirectoryIdAsync(Guid directoryId)
    {
        GetToDoDirectoryByIdResponse? directory = await _httpClient.GetToDoDirectoryByIdAsync(directoryId);

        var pdfModel = _mapper.Map<ToDoDirectoryPdfModel>(directory);

        string pageContent = await _razorProvider.RenderPageAsync("~/Views/ToDoDirectory.cshtml", pdfModel);

        Stream pdf = _pdfHelper.GeneratePdf(pageContent);

        string fileName = await _blobService.UploadAsync(pdf);

        var blobFile = new BlobFile
        {
            Name = fileName
        };

        await _blobFileRepository.CreateAsync(blobFile);

        var response = _mapper.Map<GeneratePdfByDirectoryIdResponse>(blobFile);

        response.IsSucceeded = true;

        return response;
    }

    public async Task<Stream> DownloadAsync(Guid fileId)
    {
        BlobFile? blobFile = await _blobFileRepository.GetByIdAsync(fileId);

        if (blobFile is null)
        {
            //TODO: return model with error
            throw new Exception();
        }

        Stream file = await _blobService.DownloadAsync(blobFile.Name);

        return file;
    }

    public async Task SendViaEmailAsync(Guid fileId)
    {
        BlobFile? blobFile = await _blobFileRepository.GetByIdAsync(fileId);

        Guid? userId = _httpContextAccessor.HttpContext.User.Identity!.GetUserId();

        if (blobFile is null || !userId.HasValue)
        {
            //TODO: return model with error
            throw new Exception();
        }

        var message = new SendViaEmailQueueMessageModel
        {
            UserId = userId.Value,
            FileId = fileId
        };

        string content = JsonConvert.SerializeObject(message);

        byte[] contentBytes = Encoding.UTF8.GetBytes(content);

        _messageQueueClient.Enqueue(contentBytes);
    }
}
