using EasyCart.AuthService.DAL;
using EasyCart.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using EasyCart.AuthService.DAL.Extensions;
using EasyCart.AuthService.Services.Extensions;
using EasyCart.AuthService.Models.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Adding DB context
builder.Services.AddDbContext<AuthDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EasyCartAuthDB"));
});

// Binding custom models with configurations
builder.Services.Configure<AccessTokenConfiguration>
(
    builder.Configuration.GetSection(nameof(AccessTokenConfiguration))
);
builder.Services.Configure<RefreshTokenConfiguration>
(
    builder.Configuration.GetSection(nameof(RefreshTokenConfiguration))
);

// For registering repositories for DI
builder.Services.RegisterRepositories();

// For registering shared services like - swagger, CORS etc
builder.Services.RegisterSharedServices();

// For registering services for DI
builder.Services.RegisterServices();

var app = builder.Build();

// To configure shared services like - swagger, CORS etc
app.ConfigureSharedServices();

app.Run();