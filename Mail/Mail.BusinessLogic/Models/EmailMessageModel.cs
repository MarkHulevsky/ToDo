using System.Net.Mail;

namespace Mail.BusinessLogic.Models;

public class EmailMessageModel
{
    public string Subject { get; set; }

    public string Body { get; set; }

    public MailAddress ReceiverAddress { get; set; }

    public List<Attachment> Attachments { get; set; }
}