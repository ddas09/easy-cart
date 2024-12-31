using Microsoft.AspNetCore.Http;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace EasyCart.Shared.Middlewares;

public class APIGatewayAuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiGatewaySecretKey;
    private readonly ILogger<APIGatewayAuthorizationMiddleware> _logger;
    
    public APIGatewayAuthorizationMiddleware(RequestDelegate next, ILogger<APIGatewayAuthorizationMiddleware> logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _apiGatewaySecretKey = configuration["APIGateway:SecretKey"]
            ?? throw new ApiException(message: "API Gateway secret key is missing in microservice", errorCode: AppConstants.ErrorCodeEnum.ServerError);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestPath = context.Request.Path.Value;
        
        // Skip validation swagger UI endpoint
        if (string.IsNullOrEmpty(requestPath) || requestPath.StartsWith(AppConstants.SwaggerEndpoint, StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        // Check for the presence of the API Gateway key in headers
        if (!context.Request.Headers.TryGetValue(AppConstants.APIGatewaySecretHeader, out var providedKey) || string.IsNullOrEmpty(providedKey))
        {
            _logger.LogWarning("Missing API Gateway Key in request headers.");
            throw new ApiException(message: "Forbidden: Missing API Gateway Key.", errorCode: AppConstants.ErrorCodeEnum.Forbidden);
        }

        // Validate the provided key
        if (!providedKey.Equals(_apiGatewaySecretKey))
        {
            _logger.LogWarning("Invalid API Gateway Key provided.");
            throw new ApiException(message: "Forbidden: Invalid API Gateway Key.", errorCode: AppConstants.ErrorCodeEnum.Forbidden);
        }

        // Continue request, if it's from a valid IP (API gateway)
        await _next(context);
    }
}
