using AutoMapper;
using Pdf.BusinessLogic.Helpers.Interfaces;
using Pdf.BusinessLogic.HttpClients.Interfaces;
using Pdf.BusinessLogic.HttpClients.Models.ToDo.Response;
using Pdf.BusinessLogic.Models;
using Pdf.BusinessLogic.Providers.Interfaces;
using Pdf.BusinessLogic.Services.Interfaces;

namespace Pdf.BusinessLogic.Services;

public class PdfService: IPdfService
{
    private readonly IToDoHttpClient _httpClient;
    private readonly IRazorProvider _razorProvider;
    private readonly IPdfHelper _pdfHelper;
    private readonly IMapper _mapper;

    public PdfService(IToDoHttpClient httpClient, IPdfHelper pdfHelper, IRazorProvider razorProvider, IMapper mapper)
    {
        _httpClient = httpClient;
        _pdfHelper = pdfHelper;
        _razorProvider = razorProvider;
        _mapper = mapper;
    }

    public async Task<Stream> GetByDirectoryIdAsync(Guid directoryId)
    {
        GetToDoDirectoryByIdResponse? directory = await _httpClient.GetToDoDirectoryByIdAsync(directoryId);

        var pdfModel = _mapper.Map<ToDoDirectoryPdfModel>(directory);

        string pageContent = await _razorProvider.RenderPageAsync("~/Views/ToDoDirectory.cshtml", pdfModel);

        Stream pdf = _pdfHelper.GeneratePdf(pageContent);

        return pdf;
    }
}
