using ToDo.BusinessLogic.Models.Mail;

namespace ToDo.BusinessLogic.HttpClients.Interfaces;

public interface IMailHttpClient
{
    public Task SendEmailAsync(SendEmailModel email);
}