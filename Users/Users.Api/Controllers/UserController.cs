using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.BusinessLogic.Services.Interfaces;

namespace Users.Api.Controllers;

[Authorize]
[Route("users/api/[controller]/[action]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(Guid userId)
    {
        BaseResponseModel response = await _userService.GetByIdAsync(userId);

        if (!response.IsSucceeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}