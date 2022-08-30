using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.BusinessLogic.Models.ToDoDirectory.Request;
using ToDo.BusinessLogic.Services.Interfaces;

namespace ToDo.Api.Controllers;

[Authorize]
[Route("todo/api/[controller]/[action]")]
public class ToDoDirectoryController: ControllerBase
{
    private readonly IToDoDirectoryService _toDoDirectoryService;

    public ToDoDirectoryController(IToDoDirectoryService toDoDirectoryService)
    {
        _toDoDirectoryService = toDoDirectoryService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateToDoDirectoryRequest request)
    {
        BaseResponseModel response = await _toDoDirectoryService.CreateAsync(request);

        if (!response.IsSucceeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}