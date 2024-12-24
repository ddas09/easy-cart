using System.Net;
using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc;
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
            ?? throw new ApiException(message: "API Gateway secret key is missing in shared project", AppConstants.ErrorCodeEnum.ServerError);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try 
        {
            Console.WriteLine("Hello from Shared");
            
            // Check for the presence of the API Gateway key in headers
            if (!context.Request.Headers.TryGetValue(AppConstants.APIGatewaySecretHeader, out var providedKey) || string.IsNullOrEmpty(providedKey))
            {
                _logger.LogWarning("Missing API Gateway Key in request headers.");
                await HandleResponse(context, HttpStatusCode.Forbidden, "Forbidden: Missing API Gateway Key.");
                return;
            }

            // Validate the provided key
            if (!providedKey.Equals(_apiGatewaySecretKey))
            {
                _logger.LogWarning("Invalid API Gateway Key provided.");
                await HandleResponse(context, HttpStatusCode.Forbidden, "Forbidden: Invalid API Gateway Key.");
                return;
            }

            // Continue request, if it's from a valid IP (API gateway)
            await _next(context);
        }
        catch (Exception exception) 
        {
            _logger.LogError(exception, "An exception occurred while processing the request.");
            await this.HandleResponse(context: context, statusCode: HttpStatusCode.InternalServerError, message: "Something went wrong on the server.");
        }
    }

    private async Task HandleResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {        
        var customResponse = new CustomResponse();

        ContentResult result = statusCode switch
        {
            HttpStatusCode.Forbidden => customResponse.Forbidden(message: message),
            _ => customResponse.ServerError()
        };

        context.Response.StatusCode = result.StatusCode ?? (int)HttpStatusCode.OK;
        await context.Response.WriteAsync(result.Content ?? string.Empty);
    }
}
