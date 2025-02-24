using AspNetCoreRateLimit;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using X.Application.Core.BackgroundJobs;
using X.Application.Core.Data;
using X.Application.Core.Emails;
using X.Application.Core.JWT;
using X.Application.Core.Storage;
using X.Domain.Notifications;
using X.Domain.Orders;
using X.Domain.Products;
using X.Domain.Users;
using X.Infrastructure.Database;
using X.Infrastructure.ExternalServices.BackgroundJobs;
using X.Infrastructure.ExternalServices.Emails;
using X.Infrastructure.ExternalServices.JWT;
using X.Infrastructure.ExternalServices.JWT.Settings;
using X.Infrastructure.ExternalServices.Sms;
using X.Infrastructure.ExternalServices.Sms.Settings;
using X.Infrastructure.ExternalServices.Storage;
using X.Infrastructure.Repositories;

namespace X.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Connection To Database.
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());


        // Register Services.
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();


        // Register FileStorageService
        services.AddScoped<IFileStorageService, FileStorageService>();


        // Register Services.
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ISmsService, TwilioSmsService>();
        //services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IBackgroundJobService, BackgroundJobService>();

        // Register Configuration.
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<EmailService>(configuration.GetSection("EmailService"));
        services.Configure<TwilioSettings>(configuration.GetSection("TwilioSettings"));


        // Configure Hangfire
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        services.AddHangfireServer();


        // Configure Rate Limiting
        services.AddOptions();
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();


        // Configure Elasticsearch
        var settings = new ConnectionSettings(new Uri(configuration["Elasticsearch:Uri"]))
            .DefaultIndex(configuration["Elasticsearch:Index"]);
        var client = new ElasticClient(settings);
        services.AddSingleton<IElasticClient>(client);


        return services;
    }
}
