using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Entities;

namespace ToDo.DataAccess;

public class ApplicationDbContext: DbContext
{
    public DbSet<ToDoNote> ToDoNotes { get; set; }

    public DbSet<ToDoDirectory> ToDoDirectories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
}