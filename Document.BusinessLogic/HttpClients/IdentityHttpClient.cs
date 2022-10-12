using Common.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Pdf.BusinessLogic.Constants;

namespace Pdf.BusinessLogic.HttpClients;

public class IdentityHttpClient
{
    private readonly IOptions<IdentitySettingsModel> _options;

    private readonly IHttpClientFactory _httpClientFactory;

    public IdentityHttpClient(IOptions<IdentitySettingsModel> options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClientFactory = httpClientFactory;
    }

    protected async Task<TokenResponse> GetAccessTokenAsync(string scope)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(HttpClientsConstants.IDENTITY_HTTP_CLIENT_NAME);

        DiscoveryDocumentResponse discoveryDocument = await httpClient.GetDiscoveryDocumentAsync();

        TokenResponse tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = _options.Value.ClientId,
            ClientSecret = _options.Value.ClientSecret,
            Scope = scope
        });

        return tokenResponse;
    }
}