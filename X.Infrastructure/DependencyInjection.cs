using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using X.Application.Core.Data;
using X.Application.Core.Emails;
using X.Application.Core.JWT;
using X.Domain.Notifications;
using X.Domain.Orders;
using X.Domain.Products;
using X.Domain.Users;
using X.Infrastructure.Database;
using X.Infrastructure.Emails;
using X.Infrastructure.JWT;
using X.Infrastructure.JWT.Settings;
using X.Infrastructure.Repositories;
using X.Infrastructure.Sms;
using X.Infrastructure.Sms.Settings;

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



        // Register Services.
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ISmsService, TwilioSmsService>();


        // Register Configuration.
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<EmailService>(configuration.GetSection("EmailService"));
        services.Configure<TwilioSettings>(configuration.GetSection("TwilioSettings"));


        return services;
    }
}
