using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasyCart.Shared.Extensions;

public static class ApplicationBuilder
{
    public static void ConfigureSharedServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });

            app.MapOpenApi();
        }

        // For configuring custom middlewares
        app.ConfigureSharedMiddlewares();

        app.UseHttpsRedirection();

        // Enable controller routing
        app.MapControllers();

        // Enable CORS
        app.UseCors("MyCORSPolicy");
    }
}