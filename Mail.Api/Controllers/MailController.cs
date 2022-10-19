using Mail.BusinessLogic.Models;
using Mail.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Api.Controllers;

[Authorize]
[Route("mail/api/[controller]/[action]")]
public class MailController: ControllerBase
{
    private readonly IMailService _mailService;

    public MailController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendEmail([FromBody] SendViaEmailQueueMessageModel queueMessageModel)
    {
        await _mailService.SendFileViaEmailAsync(queueMessageModel);

        return Ok();
    }
}