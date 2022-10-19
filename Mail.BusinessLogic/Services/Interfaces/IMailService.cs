using Mail.BusinessLogic.Models;

namespace Mail.BusinessLogic.Services.Interfaces;

public interface IMailService
{
    public Task SendFileViaEmailAsync(SendViaEmailQueueMessageModel queueMessageModel);
}