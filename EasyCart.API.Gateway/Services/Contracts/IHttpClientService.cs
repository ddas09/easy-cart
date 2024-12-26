namespace EasyCart.API.Gateway.Services.Contracts;

public interface IHttpClientService
{
    Task<HttpResponseMessage> PostAsync<T>(string endpoint, T payload);
}