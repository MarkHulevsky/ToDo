namespace Pdf.BusinessLogic.Models;

public class ToDoDirectoryPdfModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<ToDoDirectoryPdfNoteModel> ToDoNotes { get; set; }
}

public class ToDoDirectoryPdfNoteModel
{
    public Guid Id { get; set; }

    public string Note { get; set; }

    public bool IsCompleted { get; set; }
}
