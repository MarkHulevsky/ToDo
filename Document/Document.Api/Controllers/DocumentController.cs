using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Document.BusinessLogic.Models.Response;
using Document.BusinessLogic.Services.Interfaces;

namespace Document.Api.Controllers;

[Authorize]
[Route("document/api/[controller]/[action]")]
public class DocumentController: ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet("{directoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateByDirectoryId(Guid directoryId)
    {
        GeneratePdfByDirectoryIdResponse response = await _documentService.GenerateByDirectoryIdAsync(directoryId);

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
        Stream file = await _documentService.DownloadAsync(fileId);

        return File(file, "application/octet-stream");
    }

    [HttpGet("{fileId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendViaEmail(Guid fileId)
    {
        await _documentService.SendViaEmailAsync(fileId);

        return Ok();
    }
}