using WatermarkService.Core.Application.Interfaces.Messaging;

namespace WatermarkService.Worker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Worker> _logger;

    public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using(var scope = _serviceProvider.CreateScope())
        {
            var watermarkService = scope.ServiceProvider.GetRequiredService<IMessageWatermarkService>();
            
            watermarkService.StartListen();
        }
        
        return Task.CompletedTask;
    }
}
