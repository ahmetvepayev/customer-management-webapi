using ReportService.Core.Application.Interfaces.Messaging;

namespace ReportService.Worker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using(var scope = _serviceProvider.CreateScope())
        {
            var reportService = scope.ServiceProvider.GetRequiredService<IMessageReportService>();
            
            reportService.StartListen();
        }

        return Task.CompletedTask;
    }
}
