using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Pdf.BusinessLogic.Helpers;
using Pdf.BusinessLogic.Helpers.Interfaces;
using Pdf.BusinessLogic.Providers;
using Pdf.BusinessLogic.Providers.Interfaces;
using Pdf.BusinessLogic.Services;
using Pdf.BusinessLogic.Services.Interfaces;

namespace Pdf.BusinessLogic;

public static class Startup
{
    public static IServiceCollection ConfigureBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRazorProvider, RazorProvider>();
        serviceCollection.AddScoped<IPdfHelper, PdfHelper>();
        serviceCollection.AddScoped<IPdfService, PdfService>();

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