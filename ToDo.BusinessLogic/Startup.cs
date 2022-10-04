using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ToDo.BusinessLogic.Services;
using ToDo.BusinessLogic.Services.Interfaces;

namespace ToDo.BusinessLogic;

public static class Startup
{
    public static IServiceCollection ConfigureBusinessLogicServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IToDoDirectoryService, ToDoDirectoryService>();
        serviceCollection.AddScoped<IToDoNoteService, ToDoNoteService>();

        return serviceCollection;
    }

    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection serviceCollection)
    {
        var mapperConfiguration = new MapperConfiguration(expression =>
            expression.AddMaps(Assembly.GetExecutingAssembly())
        );

        IMapper mapper = mapperConfiguration.CreateMapper();

        serviceCollection.AddSingleton(mapper);

        return serviceCollection;
    }
}