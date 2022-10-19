using Mail.BusinessLogic.Providers;
using Mail.BusinessLogic.Providers.Interfaces;
using Mail.BusinessLogic.Services;
using Mail.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mail.BusinessLogic;

public static class Startup
{
    public static IServiceCollection ConfigureBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEmailProvider, SmtpProvider>();
        serviceCollection.AddScoped<IMailService, MailService>();

        return serviceCollection;
    }
}