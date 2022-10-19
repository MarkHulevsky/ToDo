using System.Reflection;
using AutoMapper;
using Document.BusinessLogic.Helpers;
using Document.BusinessLogic.Helpers.Interfaces;
using Document.BusinessLogic.MessageQueueClients;
using Document.BusinessLogic.MessageQueueClients.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Document.BusinessLogic.Providers;
using Document.BusinessLogic.Providers.Interfaces;
using Document.BusinessLogic.Services;
using Document.BusinessLogic.Services.Interfaces;
using DocumentService = Document.BusinessLogic.Services.DocumentService;

namespace Document.BusinessLogic;

public static class Startup
{
    public static IServiceCollection ConfigureBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureAutoMapper();

        serviceCollection.AddScoped<IRazorProvider, RazorProvider>();
        serviceCollection.AddScoped<IPdfHelper, PdfHelper>();
        serviceCollection.AddScoped<IDocumentService, DocumentService>();
        serviceCollection.AddScoped<IBlobService, BlobService>();
        serviceCollection.AddSingleton<IMessageQueueClient, RabbitMqClient>();

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