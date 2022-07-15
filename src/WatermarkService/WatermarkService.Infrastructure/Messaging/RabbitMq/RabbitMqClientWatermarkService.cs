using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace WatermarkService.Infrastructure.Messaging.RabbitMq;

public class RabbitMqClientWatermarkService : IDisposable
{
    public const string ExchangeName = "WatermarkDirectExchange";
    public const string ExchType = ExchangeType.Direct;
    public const string RoutingKey = "watermark-route";
    public const string QueueName = "watermark-queue";

    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;
    
    private readonly ILogger<RabbitMqClientWatermarkService> _logger;

    public RabbitMqClientWatermarkService(ConnectionFactory connectionFactory, ILogger<RabbitMqClientWatermarkService> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;

        Connect();
    }

    public IModel Connect()
    {
        if (_channel is { IsOpen: true })
        {
            return _channel;
        }

        _connection = _connectionFactory.CreateConnection();

        _channel = _connection.CreateModel();

        _logger.LogInformation("Watermark service connected to RabbitMQ...");


        return _channel;

    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();

        _connection?.Close();
        _connection?.Dispose();

        _logger.LogInformation("Watermark service disconnected from RabbitMQ...");

    }
}