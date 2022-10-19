namespace Mail.BusinessLogic.Models;

public class SmtpSettingsModel
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public int Port { get; set; }

    public string Server { get; set; }
}