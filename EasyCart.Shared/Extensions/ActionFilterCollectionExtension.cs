using EasyCart.Shared.ActionFilters;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCart.Shared.Extensions;

public static class ActionFilterCollection
{
    public static void RegisterActionFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidateModelStateAttribute>();
    }
}
