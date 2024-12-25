using System.Net;
using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace EasyCart.Shared.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception exception) 
        {
            this._logger.LogError(exception.ToString());
            await this.HandleException(context: context, exception: exception);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var response = new CustomResponse();

        ContentResult result = exception switch
        {
            ApiException apiException => this.HandleApiException(apiException, response),
            _ => response.ServerError()
        };

        context.Response.StatusCode = result.StatusCode ?? (int)HttpStatusCode.OK;
        await context.Response.WriteAsync(result.Content ?? string.Empty);
    }

    private ContentResult HandleApiException(ApiException apiException, CustomResponse response)
    {
        return apiException.ErrorCode switch
        {
            AppConstants.ErrorCodeEnum.Conflict => response.Conflict(message: apiException.Message),
            AppConstants.ErrorCodeEnum.NotFound => response.NotFound(message: apiException.Message),
            AppConstants.ErrorCodeEnum.BadRequest => response.BadRequest(message: apiException.Message),
            AppConstants.ErrorCodeEnum.Forbidden => response.Forbidden(message: apiException.Message),
            AppConstants.ErrorCodeEnum.Unauthorized => response.Unauthorized(message: apiException.Message),
            AppConstants.ErrorCodeEnum.InvalidAccessToken => response.Unauthorized(message: apiException.Message, status: AppConstants.InvalidAccessToken),
            AppConstants.ErrorCodeEnum.InvalidRefreshToken => response.Unauthorized(message: apiException.Message, status: AppConstants.InvalidRefreshToken),
            _ => response.ServerError()
        };
    }
}

