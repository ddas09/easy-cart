using System.Net;
using EasyCart.Shared.Models;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Exceptions;

namespace EasyCart.API.Gateway.Middlewares;

public class APIKeyInjectingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiGatewaySecretKey;
    private readonly ILogger<APIKeyInjectingMiddleware> _logger;

    public APIKeyInjectingMiddleware(RequestDelegate next, ILogger<APIKeyInjectingMiddleware> logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _apiGatewaySecretKey = configuration["APIGateway:SecretKey"]
            ?? throw new ApiException(message: "API Gateway secret key is missing in API gateway project", AppConstants.ErrorCodeEnum.ServerError);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.Headers.Append(AppConstants.APIGatewaySecretHeader, _apiGatewaySecretKey);            
            await _next(context);
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception.ToString());
            await this.HandleResponse(context: context);
        }
    }

    private async Task HandleResponse(HttpContext context)
    {
        var result = new CustomResponse().ServerError();
        context.Response.StatusCode = result.StatusCode ?? (int)HttpStatusCode.OK;
        await context.Response.WriteAsync(result.Content ?? string.Empty);
    }
}