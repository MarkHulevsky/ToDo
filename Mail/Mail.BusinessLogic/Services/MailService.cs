using System.Net.Mail;
using Mail.BusinessLogic.Constants;
using Mail.BusinessLogic.HttpClients.Interfaces;
using Mail.BusinessLogic.HttpClients.Models.User.Response;
using Mail.BusinessLogic.Models;
using Mail.BusinessLogic.Providers.Interfaces;
using Mail.BusinessLogic.Services.Interfaces;

namespace Mail.BusinessLogic.Services;

public class MailService: IMailService
{
    private readonly IDocumentHttpClient _documentHttpClient;
    private readonly IUserHttpClient _userHttpClient;
    private readonly IEmailProvider _emailProvider;

    public MailService(IDocumentHttpClient documentHttpClient, IUserHttpClient userHttpClient, IEmailProvider emailProvider)
    {
        _documentHttpClient = documentHttpClient;
        _userHttpClient = userHttpClient;
        _emailProvider = emailProvider;
    }

    public async Task SendFileViaEmailAsync(SendViaEmailQueueMessageModel queueMessageModel)
    {
        Stream file = await _documentHttpClient.DownloadFileAsync(queueMessageModel.FileId);

        GetUserByIdResponse user = (await _userHttpClient.GetByIdAsync(queueMessageModel.UserId))!;

        var message = new EmailMessageModel
        {
            Subject = EmailConstants.SEND_FILE_VIA_EMAIL_SUBJECT,
            Body = string.Empty,
            ReceiverAddress = new MailAddress(user.Email),
            Attachments = new List<Attachment>
            {
                new(file, "Todo_list")
            }
        };

        await _emailProvider.SendAsync(message);
    }
}