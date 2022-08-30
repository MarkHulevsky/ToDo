using Mail.BusinessLogic.Models.Request;

namespace Mail.BusinessLogic.Services.Interfaces;

public interface IMailService
{
    public Task SendEmailAsync(SendEmailRequest request);
}