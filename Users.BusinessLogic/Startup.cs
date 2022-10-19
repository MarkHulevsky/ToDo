using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Users.BusinessLogic.Services;
using Users.BusinessLogic.Services.Interfaces;

namespace Users.BusinessLogic;

public static class Startup
{
    public static IServiceCollection ConfigureBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureAutoMapper();

        serviceCollection.AddScoped<IAccountService, AccountService>();

        return serviceCollection;
    }

    private static IServiceCollection ConfigureAutoMapper(this IServiceCollection serviceCollection)
    {
        var mapperConfiguration = new MapperConfiguration(expression =>
            expression.AddMaps(Assembly.GetExecutingAssembly())
        );

        IMapper mapper = mapperConfiguration.CreateMapper();

        serviceCollection.AddSingleton(mapper);

        return serviceCollection;
    }
}