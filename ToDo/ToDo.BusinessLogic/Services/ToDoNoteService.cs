using AutoMapper;
using ToDo.BusinessLogic.Models.ToDoNote.Request;
using ToDo.BusinessLogic.Models.ToDoNote.Response;
using ToDo.BusinessLogic.Services.Interfaces;
using ToDo.DataAccess.Entities;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.BusinessLogic.Services;

public class ToDoNoteService: IToDoNoteService
{
    private readonly IToDoNoteRepository _toDoNoteRepository;
    private readonly IMapper _mapper;

    public ToDoNoteService(IToDoNoteRepository toDoNoteRepository, IMapper mapper)
    {
        _toDoNoteRepository = toDoNoteRepository;
        _mapper = mapper;
    }

    public async Task<GetAllToDoNotesByDirectoryIdResponse> GetAllByDirectoryIdAsync(Guid directoryId)
    {
        IList<ToDoNote> notes = await _toDoNoteRepository.GetAllByDirectoryIdAsync(directoryId);

        var response = new GetAllToDoNotesByDirectoryIdResponse
        {
            ToDoNotes = _mapper.Map<IEnumerable<GetAllToDoNotesByDirectoryIdResponseItemModel>>(notes)
        };

        return response;
    }

    public async Task CreateAsync(CreateToDoNoteRequest request)
    {
        var note = new ToDoNote
        {
            ToDoDirectoryId = request.ToDoDirectoryId,
            Note = request.Note
        };

        await _toDoNoteRepository.CreateAsync(note);
    }

    public async Task UpdateAsync(UpdateToDoNoteRequest request)
    {
        ToDoNote? toDoNote = await _toDoNoteRepository.GetByIdAsync(request.Id);

        if (toDoNote is null)
        {
            return;
        }

        toDoNote.IsCompleted = request.IsCompleted;
        toDoNote.Note = request.Note;

        await _toDoNoteRepository.UpdateAsync(toDoNote);
    }
}