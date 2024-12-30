using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using EasyCart.Shared.Extensions;
using EasyCart.ProductService.DAL;
using Microsoft.EntityFrameworkCore;
using EasyCart.ProductService.DAL.Extensions;
using EasyCart.ProductService.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add controllers to the container.
builder.Services.AddControllers();

// This is to configure custom ModelState validation filter
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Easy Cart Product API", Version = "v1" });

    // To include XML comments in Swagger UI
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// For generating lowecase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Adding DB context
builder.Services.AddDbContext<ProductDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EasyCartProductDB"));
});

// For registering shared action filters
builder.Services.RegisterSharedActionFilters();

// For registering repositories for DI
builder.Services.RegisterRepositories();

// For registering services for DI
builder.Services.RegisterServices();

// For AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// For configuring CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCORSPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

// For configuring custom middlewares
app.ConfigureSharedMiddlewares();

app.UseHttpsRedirection();

// Enable controller routing
app.MapControllers();

// Enable CORS
app.UseCors("MyCORSPolicy");

app.Run();
