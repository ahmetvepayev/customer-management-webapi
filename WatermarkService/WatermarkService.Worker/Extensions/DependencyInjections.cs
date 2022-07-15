using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using WatermarkService.Core.Application.Interfaces;
using WatermarkService.Core.Application.Interfaces.Messaging;
using WatermarkService.Core.Application.Services;
using WatermarkService.Core.Domain.Interfaces;
using WatermarkService.Infrastructure.Database;
using WatermarkService.Infrastructure.Messaging;
using WatermarkService.Infrastructure.Messaging.RabbitMq;
using WatermarkService.Infrastructure.Repositories;

namespace WatermarkService.Worker.Extensions;

public static class DependencyInjections
{
    public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
    {
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMessageWatermarkService, MessageWatermarkService>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(config.GetConnectionString("NpgCon")),
            ServiceLifetime.Scoped
        );

        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddSingleton<ConnectionFactory>(new ConnectionFactory{
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            Port = 5672,
            RequestedConnectionTimeout = TimeSpan.FromSeconds(15)
        });

        services.AddSingleton<RabbitMqClientWatermarkService>();
        
        return services;
    }
}