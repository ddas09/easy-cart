using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using EasyCart.API.Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot and other services
builder.Services.AddOcelot();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Configure middlewares
app.ConfigureMiddlewares();

// Use Ocelot middleware
await app.UseOcelot();

app.Run();
