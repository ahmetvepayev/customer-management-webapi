using Microsoft.EntityFrameworkCore;
using WatermarkService.Core.Application.Interfaces;
using WatermarkService.Core.Application.Services;
using WatermarkService.Core.Domain.Interfaces;
using WatermarkService.Infrastructure.Database;
using WatermarkService.Infrastructure.Repositories;
using WatermarkService.Worker;
using WatermarkService.Worker.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostcontext, services) =>
    {
        services.AddHostedServices();
        
        services.AddDatabase(hostcontext.Configuration);

        services.AddServicesAndRepositories();

        services.AddRabbitMq();
    })
    .Build();

await host.RunAsync();
