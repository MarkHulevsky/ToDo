using Common.Models;
using IdentityModel.Client;
using Mail.BusinessLogic.Constants;
using Microsoft.Extensions.Options;

namespace Mail.BusinessLogic.HttpClients;

public abstract class IdentityHttpClient
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