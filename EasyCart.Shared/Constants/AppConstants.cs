﻿namespace EasyCart.Shared.Constants;

public static class AppConstants
{
    public const string IdClaim = "id";
    public const string BearerKey = "Bearer ";
    public const string AuthorizationHeaderKey = "Authorization";
    public const string APIGatewaySecretHeader = "X-API-Gateway-Key";
    public const string UserRoleHeaderKey = "X-User-Role";

    public const string ContentType = "text/json";

    // possible statuses in server response
    public const string Success = "success";
    public const string Created = "created";
    public const string Conflict = "conflict";
    public const string NotFound = "not_found";
    public const string Forbidden = "forbidden";
    public const string BadRequest = "bad_request";
    public const string ServerError = "server_error";
    public const string Unauthorized = "unauthorized";
    public const string TokenRequired = "token_required";
    public const string InvalidAccessToken = "invalid_access_token";
    public const string InvalidRefreshToken = "invalid_refresh_token";

    public enum ErrorCodeEnum
    {
        NotFound,
        Conflict,
        BadRequest,
        Forbidden,
        ServerError,
        Unauthorized,
        InvalidAccessToken,
        InvalidRefreshToken
    }

    public const string SwaggerEndpoint = "/openapi";

    public static readonly List<string> PublicEndpoints =
    [
        "/auth/login",
        "/auth/register",
        "/auth/tokens/refresh"
    ];

    public const string AuthServiceBaseURL = "http://localhost:5001";
}

