using EasyCart.Shared.ActionFilters;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCart.Shared.Extensions;

internal static class ActionFilterCollection
{
    internal static void RegisterSharedActionFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidateModelStateAttribute>();
    }
}
