using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using EasyCart.API.Gateway.Extensions;
using EasyCart.API.Gateway.Services.Extensions;
using EasyCart.API.Gateway.Models.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot and other services
builder.Services.AddOcelot();

// For configuring CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiGatewayCORSPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Binding custom models with configurations
builder.Services.Configure<AuthTokenConfiguration>
(
    builder.Configuration.GetSection(nameof(AuthTokenConfiguration))
);

// For registering services for DI
builder.Services.RegisterServices();

var app = builder.Build();

app.UseHttpsRedirection();

// Configure middlewares
app.ConfigureMiddlewares();

// Enable CORS
app.UseCors("ApiGatewayCORSPolicy");

// Use Ocelot middleware
await app.UseOcelot();

app.Run();
