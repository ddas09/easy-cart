using EasyCart.API.Gateway.Middlewares;

namespace EasyCart.API.Gateway.Extensions;

public static class MiddlewareCollection
{
    public static void ConfigureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<APIKeyInjectingMiddleware>();
    }
}


