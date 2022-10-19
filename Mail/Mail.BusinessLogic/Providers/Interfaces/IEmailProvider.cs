using Mail.BusinessLogic.Models;

namespace Mail.BusinessLogic.Providers.Interfaces;

public interface IEmailProvider
{
    Task SendAsync(EmailMessageModel message);
}