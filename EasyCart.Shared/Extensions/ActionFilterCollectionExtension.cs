using EasyCart.Shared.ActionFilters;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCart.Shared.Extensions;

public static class ActionFilterCollection
{
    public static void RegisterSharedActionFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidateModelStateAttribute>();
    }
}
