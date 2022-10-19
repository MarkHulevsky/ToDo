using Microsoft.AspNetCore.Mvc;
using Users.BusinessLogic.Models.Account.Request;
using Users.BusinessLogic.Services.Interfaces;

namespace Users.Api.Controllers;

[Route("users/api/[controller]/[action]")]
public class AccountController: ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        await _accountService.CreateAsync(request);

        return Ok();
    }
}