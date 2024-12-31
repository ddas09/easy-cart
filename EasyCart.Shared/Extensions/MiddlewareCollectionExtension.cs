using EasyCart.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EasyCart.Shared.Extensions;

internal static class MiddlewareCollection
{
    internal static void ConfigureSharedMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseMiddleware<APIGatewayAuthorizationMiddleware>();
    }
}


