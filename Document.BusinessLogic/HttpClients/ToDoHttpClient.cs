using System.Net.Http.Json;
using System.Text.Json;
using Common.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Pdf.BusinessLogic.Constants;
using Pdf.BusinessLogic.HttpClients.Interfaces;
using Pdf.BusinessLogic.HttpClients.Models.ToDo.Response;

namespace Pdf.BusinessLogic.HttpClients;

public class ToDoHttpClient: IdentityHttpClient, IToDoHttpClient
{
    private readonly HttpClient _httpClient;

    public ToDoHttpClient(IOptions<IdentitySettingsModel> options, IHttpClientFactory httpClientFactory,
        HttpClient httpClient) : base(options, httpClientFactory)
    {
        _httpClient = httpClient;
    }

    public async Task<GetToDoDirectoryByIdResponse?> GetToDoDirectoryByIdAsync(Guid directoryId)
    {
        TokenResponse tokenResponse = await GetAccessTokenAsync("TodoAPI");

        _httpClient.SetBearerToken(tokenResponse.AccessToken);

        HttpResponseMessage response = await _httpClient.GetAsync($"{ToDoHttpClientConstants.GET_DIRECTORY_BY_ID_URL}{directoryId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        object? directory = await response.Content.ReadFromJsonAsync(typeof(GetToDoDirectoryByIdResponse));

        return directory as GetToDoDirectoryByIdResponse;
    }
}