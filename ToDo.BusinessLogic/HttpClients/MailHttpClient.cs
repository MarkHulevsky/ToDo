using System.Text.Json;
using ToDo.BusinessLogic.Constants;
using ToDo.BusinessLogic.HttpClients.Interfaces;
using ToDo.BusinessLogic.Models.Mail;

namespace ToDo.BusinessLogic.HttpClients;

public class MailHttpClient: IMailHttpClient
{
    private readonly HttpClient _httpClient;

    public MailHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendEmailAsync(SendEmailModel email)
    {
        var content = new StringContent(JsonSerializer.Serialize(email));

        HttpResponseMessage response = await _httpClient.PostAsync(MailHttpClientConstants.SEND_EMAIL_PATH, content);
    }
}