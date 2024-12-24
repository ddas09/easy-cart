using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using EasyCart.Shared.ActionFilters;
using EasyCart.AuthService.Models.Request;
using EasyCart.AuthService.Services.Contracts;

namespace EasyCart.AuthService.Controllers;

/// <summary>
/// Controller responsible for managing user authentication, onboaring.
/// </summary>
/// <param name="authService">The auth service for handling user auth related operations.</param>
[ApiController]
[ValidateModelState]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{   
    private readonly CustomResponse _customResponse = new();
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Authenticates a user and returns user informations.
    /// </summary>
    /// <param name="request">The request containing user credentials.</param>
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var authResponse = await _authService.LoginUser(request);
        return _customResponse.Success(message: "Logged in successfully.", data: authResponse);
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="request">The register request containing user details.</param>
    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var authResponse = await _authService.RegisterUser(request);
        return _customResponse.Success(message: "Account created successfully.", data: authResponse);
    }
}