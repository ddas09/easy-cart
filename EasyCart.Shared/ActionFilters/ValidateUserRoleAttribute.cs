using System.Security.Claims;
using EasyCart.Shared.Constants;
using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyCart.Shared.ActionFilters;

public class ValidateUserRoleAttribute : ActionFilterAttribute, IActionFilter
{
    private readonly CustomResponse _customResponse;

    public string[] AllowedRoles { get; set; } = [];

    public ValidateUserRoleAttribute()
    {
        _customResponse = new CustomResponse();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var roleClaim = context.HttpContext.Request.Headers[AppConstants.UserRoleHeaderKey].FirstOrDefault();

        // If the 'role' claim is found in the header, proceed
        if (string.IsNullOrEmpty(roleClaim))
        {
            context.Result = _customResponse.Unauthorized(message: "Role is missing from request header.");
            return;
        }

        // Check if the user's role matches any of the allowed roles
        if (!AllowedRoles.Contains(roleClaim))
        {
            context.Result = _customResponse.Unauthorized(message: "User does not have access to the requested resource.");
        }
    }
}
