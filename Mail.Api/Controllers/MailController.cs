using Mail.BusinessLogic.Models.Request;
using Mail.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Api.Controllers;

[Route("mail/api/[controller]/[action]")]
[Authorize]
public class MailController: ControllerBase
{
    private readonly IMailService _mailService;

    public MailController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        await _mailService.SendEmailAsync(request);

        return Ok();
    }
}