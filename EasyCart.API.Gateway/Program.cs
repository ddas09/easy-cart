using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using EasyCart.API.Gateway.Extensions;
using EasyCart.Shared.Services.Extensions;
using EasyCart.API.Gateway.Models.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot and other services
builder.Services.AddOcelot();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// For configuring CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCORSPolicy", policy =>
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

// For registering shared services
builder.Services.RegisterSharedServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Configure middlewares
app.ConfigureMiddlewares();

// Enable CORS
app.UseCors("MyCORSPolicy");

// Use Ocelot middleware
await app.UseOcelot();

app.Run();
