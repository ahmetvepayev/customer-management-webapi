using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Interfaces.AuthServices;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Application.Services.EntityServices;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Infrastructure.Database;
using CustomerManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql(config.GetConnectionString("NpgCon"), action => {
                action.MigrationsAssembly("CustomerManagement.Infrastructure");
            });
        });
        
        return services;
    }

    public static IServiceCollection AddIdentityFramework(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        
        return services;
    }
}