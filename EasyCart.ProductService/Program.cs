using EasyCart.Shared.Extensions;
using EasyCart.ProductService.DAL;
using Microsoft.EntityFrameworkCore;
using EasyCart.ProductService.DAL.Extensions;
using EasyCart.ProductService.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adding DB context
builder.Services.AddDbContext<ProductDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EasyCartProductDB"));
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