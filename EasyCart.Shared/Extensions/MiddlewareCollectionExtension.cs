using EasyCart.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EasyCart.Shared.Extensions;

public static class MiddlewareCollection
{
    public static void ConfigureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseMiddleware<APIGatewayAuthorizationMiddleware>();
    }
}


