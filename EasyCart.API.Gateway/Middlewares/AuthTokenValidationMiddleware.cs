using System.Net;
using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using EasyCart.Shared.Constants;
using Microsoft.Extensions.Options;
using EasyCart.Shared.Services.Contracts;
using EasyCart.API.Gateway.Models.Configurations;

namespace EasyCart.API.Gateway.Middlewares;

public class AuthTokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly CustomResponse _customResponse = new();
    private readonly AuthTokenConfiguration _authTokenConfiguration;

    private readonly ILogger<AuthTokenValidationMiddleware> _logger;

    public AuthTokenValidationMiddleware
    (
        RequestDelegate next, 
        ILogger<AuthTokenValidationMiddleware> logger, 
        IOptions<AuthTokenConfiguration> authTokenConfiguration)
    {
        _next = next;
        _logger = logger;
        _authTokenConfiguration = authTokenConfiguration.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var requestPath = context.Request.Path.Value;

            // Skip token validation for public endpoints
            if (string.IsNullOrEmpty(requestPath) || AppConstants.PublicEndpoints.Any(endpoint => requestPath.StartsWith(endpoint, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }

            // Extract the Authorization header
            if (!context.Request.Headers.TryGetValue(AppConstants.AuthorizationHeaderKey, out var authHeader))
            {
                var result = _customResponse.Unauthorized(message: "Access token is required.", status: AppConstants.TokenRequired);
                await this.HandleResponse(result: result, context: context);
                
                return;
            }

            var jwtService = context.RequestServices.GetService<IJwtService>()
                ?? throw new InvalidOperationException($"Could not get service of type {nameof(IJwtService)}");

            var accessToken = authHeader.ToString().Replace(AppConstants.BearerKey, string.Empty);

            // Continue with the request if the token is valid
            if (jwtService.IsValidToken(configuration: _authTokenConfiguration, token: accessToken))
            {
                await _next(context);
            }
            // If access token is invalid it's FE's responsibility to accuquire new set of tokens
            // by calling the auth/tokens/refresh endpoint (using a interceptor or something)
            else
            {
                var result = _customResponse.Unauthorized(message: "Invalid access token provided.", status: AppConstants.InvalidAccessToken);
                await this.HandleResponse(result: result, context: context);
            }
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex.ToString());
            await this.HandleResponse(result: _customResponse.ServerError(), context: context);
        }
    }

    private async Task HandleResponse(ContentResult result, HttpContext context)
    {
        context.Response.StatusCode = result.StatusCode ?? (int)HttpStatusCode.OK;
        await context.Response.WriteAsync(result.Content ?? string.Empty);
    }
}