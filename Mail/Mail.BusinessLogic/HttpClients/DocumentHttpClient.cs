using Common.Models;
using IdentityModel.Client;
using Mail.BusinessLogic.Constants;
using Mail.BusinessLogic.HttpClients.Interfaces;
using Microsoft.Extensions.Options;

namespace Mail.BusinessLogic.HttpClients;

public class DocumentHttpClient : IdentityHttpClient, IDocumentHttpClient
{
    private readonly HttpClient _httpClient;

    public DocumentHttpClient(IOptions<IdentitySettingsModel> options, IHttpClientFactory httpClientFactory,
        HttpClient httpClient) : base(options, httpClientFactory)
    {
        _httpClient = httpClient;
    }

    public async Task<Stream> DownloadFileAsync(Guid fileId)
    {
        TokenResponse tokenResponse = await GetAccessTokenAsync("DocumentAPI");

        _httpClient.SetBearerToken(tokenResponse.AccessToken);

        HttpResponseMessage response =
            await _httpClient.GetAsync($"{HttpClientsConstants.DOCUMENT_DOWNLOAD_FILE_URL}{fileId}");

        if (!response.IsSuccessStatusCode)
        {
            return Stream.Null;
        }

        Stream file = await response.Content.ReadAsStreamAsync();

        return file;
    }
}