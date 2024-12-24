using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyCart.Shared.ActionFilters;

public class ValidateModelStateAttribute : ActionFilterAttribute, IActionFilter
{
    private readonly CustomResponse _customResponse;

    public ValidateModelStateAttribute()
    {
        _customResponse = new CustomResponse();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = _customResponse.BadRequest(context.ModelState);
        }
    }
}

