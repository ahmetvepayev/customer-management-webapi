using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Auth.Jwt;
using CustomerManagement.Core.Application.Interfaces.AuthServices;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Application.Interfaces.Messaging;
using CustomerManagement.Core.Application.Scheduling.QuartzJobs;
using CustomerManagement.Core.Application.Services.EntityServices;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Infrastructure.Database;
using CustomerManagement.Infrastructure.Database.Seed;
using CustomerManagement.Infrastructure.Messaging;
using CustomerManagement.Infrastructure.Messaging.RabbitMq;
using CustomerManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using RabbitMQ.Client;

namespace CustomerManagement.Api.Extensions;

public static class DependencyInjections
{
    public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICommercialTransactionService, CommercialTransactionService>();
        services.AddScoped<ICommercialTransactionRepository, CommercialTransactionRepository>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMessagePhotoWatermarkService, MessagePhotoWatermarkService>();
        services.AddScoped<IMessageReportService, MessageReportService>();
        services.AddScoped<IReportsListService, ReportsListService>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IReportFileRepository, ReportFileRepository>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql(config.GetConnectionString("NpgCon"), action => {
                action.MigrationsAssembly("CustomerManagement.Infrastructure");
            });
        });

        services.AddScoped<DatabaseSeeder>();
        
        return services;
    }

    public static IServiceCollection AddIdentityFramework(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        
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
        services.AddSingleton<RabbitMqClientReportService>();
        
        return services;
    }

    public static IServiceCollection AddQuartzSchedules(this IServiceCollection services)
    {
        services.AddQuartz(config => {
            config.UseMicrosoftDependencyInjectionJobFactory();

            config.ScheduleJob<WeeklyReportJob>(trigger => trigger
                .WithIdentity("WeeklyTrigger", "Messaging")
                .StartAt(DateTimeOffset.UtcNow.AddMinutes(5))
                .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Monday, 0, 0))
            );

            config.ScheduleJob<MonthlyReportJob>(trigger => trigger
                .WithIdentity("MonthlyTrigger", "Messaging")
                .StartAt(DateTimeOffset.UtcNow.AddMinutes(5))
                .WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(1, 0, 0))
            );
        });

        services.AddQuartzServer(options => {
            options.WaitForJobsToComplete = true;
        });
        
        return services;
    }
}