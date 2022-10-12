using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pdf.BusinessLogic.Services.Interfaces;

namespace Pdf.Api.Controllers;

[Authorize]
[Route("pdf/api/[controller]/[action]")]
public class PdfController: ControllerBase
{
    private readonly IPdfService _pdfService;

    public PdfController(IPdfService pdfService)
    {
        _pdfService = pdfService;
    }

    [HttpGet("{directoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDirectoryId(Guid directoryId)
    {
        Stream response = await _pdfService.GetByDirectoryIdAsync(directoryId);

        return File(response, "application/octet-stream");
    }
}