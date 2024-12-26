using EasyCart.API.Gateway.Services.Contracts;

namespace EasyCart.API.Gateway.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IHttpClientService, HttpClientService>()
            .ConfigureHttpClient(client => 
            {
                // add global configurations here
                client.Timeout = TimeSpan.FromSeconds(30);
            });
    }
}