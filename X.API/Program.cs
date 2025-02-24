namespace X.API;

using X.Infrastructure;
using X.Application;
using Serilog;
using Hangfire;
using AspNetCoreRateLimit;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        // Register the Swagger generator
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseSerilog((context, configuration) 
            => configuration.ReadFrom.Configuration(context.Configuration));

        var app = builder.Build();

        // Enable Swagger for all environments
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Loads Swagger at the root URL
        });

        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Use Hangfire Dashboard
        app.UseHangfireDashboard();

        // Use Rate Limiting
        app.UseIpRateLimiting();

        app.Run();
    }
}
