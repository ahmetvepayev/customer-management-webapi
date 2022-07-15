using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using ReportService.Core.Application.Interfaces;
using ReportService.Core.Application.Interfaces.Messaging;
using ReportService.Core.Application.Services;
using ReportService.Core.Domain.Interfaces;
using ReportService.Core.Domain.Interfaces.Repositories;
using ReportService.Infrastructure.Database;
using ReportService.Infrastructure.Messaging;
using ReportService.Infrastructure.Messaging.RabbitMq;
using ReportService.Infrastructure.Repositories;

namespace ReportService.Worker.Extensions;

public static class DependencyInjections
{
    public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMessageReportService, MessageReportService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICreateReportService, CreateReportService>();

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

        services.AddSingleton<RabbitMqClientReportService>();
        
        return services;
    }
}