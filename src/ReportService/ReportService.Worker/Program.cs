using ReportService.Worker;
using ReportService.Worker.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostcontext, services) =>
    {
        services.AddHostedServices();
        services.AddServicesAndRepositories();
        services.AddDatabase(hostcontext.Configuration);
        services.AddRabbitMq();
    })
    .Build();

await host.RunAsync();
