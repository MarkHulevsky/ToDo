namespace Mail.BusinessLogic.Models.Request;

public class SendEmailRequest
{
    public string SenderEmail { get; set; }

    public string ReceiverEmail { get; set; }

    public string EmailSubject { get; set; }

    public string EmailBody { get; set; }
}