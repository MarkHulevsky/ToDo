using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.BusinessLogic.Models.ToDoNote.Request;
using ToDo.BusinessLogic.Models.ToDoNote.Response;
using ToDo.BusinessLogic.Services.Interfaces;

namespace ToDo.Api.Controllers;

[Authorize]
[Route("todo/api/[controller]/[action]")]
public class ToDoNoteController: ControllerBase
{
    private readonly IToDoNoteService _toDoNoteService;

    public ToDoNoteController(IToDoNoteService toDoNoteService)
    {
        _toDoNoteService = toDoNoteService;
    }

    [HttpGet("{directoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByDirectoryId(Guid directoryId)
    {
        GetAllToDoNotesByDirectoryIdResponse response = await _toDoNoteService.GetAllByDirectoryIdAsync(directoryId);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateToDoNoteRequest request)
    {
        await _toDoNoteService.CreateAsync(request);

        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] UpdateToDoNoteRequest request)
    {
        await _toDoNoteService.UpdateAsync(request);

        return Ok();
    }
}