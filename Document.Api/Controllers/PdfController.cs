using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Document.BusinessLogic.Models.Response;
using Document.BusinessLogic.Services.Interfaces;

namespace Document.Api.Controllers;

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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateByDirectoryId(Guid directoryId)
    {
        GeneratePdfByDirectoryIdResponse response = await _pdfService.GenerateByDirectoryIdAsync(directoryId);

        if (!response.IsSucceeded)
        {
            BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("{fileId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DownloadFile(Guid fileId)
    {
        Stream file = await _pdfService.DownloadAsync(fileId);

        return File(file, "application/octet-stream");
    }
}