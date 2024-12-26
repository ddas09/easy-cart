using System.Net;
using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using EasyCart.Shared.Constants;
using EasyCart.API.Gateway.Services.Contracts;

namespace EasyCart.API.Gateway.Middlewares;

public class AuthTokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpClientService _httpClientService;
    private readonly CustomResponse _customResponse = new();
    private readonly ILogger<AuthTokenValidationMiddleware> _logger;

    public AuthTokenValidationMiddleware(RequestDelegate next, ILogger<AuthTokenValidationMiddleware> logger, IHttpClientService httpClientService)
    {
        _next = next;
        _logger = logger;
        _httpClientService = httpClientService;
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

            // Send token for validation to auth service
            var response = await _httpClientService
                .PostAsync
                (
                    endpoint: "auth/tokens/validate", 
                    payload: new { token = authHeader.ToString().Replace("Bearer ", "") }
                );
            
            // Continue with the request if the token is valid
            if (response.IsSuccessStatusCode)
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