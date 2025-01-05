using EasyCart.OrderService.DAL;
using EasyCart.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using EasyCart.OrderService.DAL.Extensions;
using EasyCart.OrderService.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adding DB context
builder.Services.AddDbContext<OrderDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EasyCartOrderDB"));
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