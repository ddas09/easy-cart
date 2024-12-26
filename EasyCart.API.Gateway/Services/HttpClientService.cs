using System.Text;
using System.Text.Json;
using EasyCart.Shared.Constants;
using EasyCart.API.Gateway.Services.Contracts;

namespace EasyCart.API.Gateway.Services;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(AppConstants.AuthServiceBaseURL);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T payload)
    {
        var jsonContent = JsonSerializer.Serialize(payload);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync(endpoint, httpContent);
    }
}