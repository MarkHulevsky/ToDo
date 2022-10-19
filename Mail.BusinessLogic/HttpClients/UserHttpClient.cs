using System.Net.Http.Json;
using Common.Models;
using IdentityModel.Client;
using Mail.BusinessLogic.Constants;
using Mail.BusinessLogic.HttpClients.Interfaces;
using Mail.BusinessLogic.HttpClients.Models.User.Response;
using Microsoft.Extensions.Options;

namespace Mail.BusinessLogic.HttpClients;

public class UserHttpClient : IdentityHttpClient, IUserHttpClient
{
    private readonly HttpClient _httpClient;

    public UserHttpClient(IOptions<IdentitySettingsModel> options, IHttpClientFactory httpClientFactory,
        HttpClient httpClient) : base(options,
        httpClientFactory)
    {
        _httpClient = httpClient;
    }

    public async Task<GetUserByIdResponse?> GetByIdAsync(Guid userId)
    {
        TokenResponse tokenResponse = await GetAccessTokenAsync("UsersAPI");
        
        _httpClient.SetBearerToken(tokenResponse.AccessToken);

        HttpResponseMessage response = await _httpClient.GetAsync($"{HttpClientsConstants.USERS_GET_BY_ID_URL}{userId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        object? user = await response.Content.ReadFromJsonAsync(typeof(GetUserByIdResponse));
        
        return user as GetUserByIdResponse;
    }
}