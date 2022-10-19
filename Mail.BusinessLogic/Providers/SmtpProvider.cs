using Mail.BusinessLogic.Models;
using Mail.BusinessLogic.Providers.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Mail.BusinessLogic.Providers;

public class SmtpProvider: IEmailProvider
{
    private readonly IOptions<SmtpSettingsModel> _smtpSettings;

    public SmtpProvider(IOptions<SmtpSettingsModel> smtpSettings)
    {
        _smtpSettings = smtpSettings;
    }

    public async Task SendAsync(EmailMessageModel message)
    {
        var mimeMessage = new MimeMessage
        {
            Body = new TextPart("html")
            {
                Text = message.Body
            },
            Subject = message.Subject
        };

        mimeMessage.From.Add(new MailboxAddress("System", _smtpSettings.Value.UserName));
        mimeMessage.To.Add(new MailboxAddress(message.ReceiverAddress.DisplayName, message.ReceiverAddress.Address));

        await SendAsync(mimeMessage);
    }

    private async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();

        client.ServerCertificateValidationCallback = (_, _, _, _) => true;

        await client.ConnectAsync(_smtpSettings.Value.Server, _smtpSettings.Value.Port, true);
        await client.AuthenticateAsync(_smtpSettings.Value.UserName, _smtpSettings.Value.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}