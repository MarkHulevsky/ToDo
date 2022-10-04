using Microsoft.Extensions.DependencyInjection;
using ToDo.DataAccess.Repositories;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess;

public static class Startup
{
    public static IServiceCollection ConfigureDataAccessServices(this IServiceCollection serviceCollection)
    {
       serviceCollection.AddScoped<IToDoDirectoryRepository, ToDoDirectoryRepository>();
       serviceCollection.AddScoped<IToDoNoteRepository, ToDoNoteRepository>();

       return serviceCollection;
    }
}