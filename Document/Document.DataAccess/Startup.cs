using Document.DataAccess.Repositories;
using Document.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Document.DataAccess;

public static class Startup
{
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBlobFileRepository, BlobFileRepository>();

        return serviceCollection;
    }
}