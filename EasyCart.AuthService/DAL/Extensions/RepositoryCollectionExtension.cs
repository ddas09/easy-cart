using EasyCart.AuthService.DAL.Contracts;
using EasyCart.AuthService.DAL.Repositories;

namespace EasyCart.AuthService.DAL.Extensions;

public static class RepositoryCollection
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRefreshTokenEntryRepository, RefreshTokenEntryRepository>();
    }
}
