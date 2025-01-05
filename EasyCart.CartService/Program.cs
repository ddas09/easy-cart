using EasyCart.CartService.DAL;
using EasyCart.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using EasyCart.CartService.DAL.Extensions;
using EasyCart.CartService.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adding DB context
builder.Services.AddDbContext<CartDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EasyCartCartDB"));
});

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