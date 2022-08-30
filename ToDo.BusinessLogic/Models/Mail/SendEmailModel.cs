namespace ToDo.BusinessLogic.Models.Mail;

public class SendEmailModel
{
    public string SenderEmail { get; set; }

    public string ReceiverEmail { get; set; }

    public string EmailSubject { get; set; }

    public string EmailBody { get; set; }
}