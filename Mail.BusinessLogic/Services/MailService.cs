using Mail.BusinessLogic.Models.Request;
using Mail.BusinessLogic.Services.Interfaces;

namespace Mail.BusinessLogic.Services;

public class MailService: IMailService
{
    public Task SendEmailAsync(SendEmailRequest request)
    {
        throw new NotImplementedException();
    }
}