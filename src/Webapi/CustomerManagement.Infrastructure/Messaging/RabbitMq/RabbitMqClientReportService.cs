using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CustomerManagement.Infrastructure.Messaging.RabbitMq;

public class RabbitMqClientReportService : IDisposable
{
    public const string ExchangeName = "ReportDirectExchange";
    public const string ExchType = ExchangeType.Direct;
    public const string RoutingKey = "report-route";
    public const string QueueName = "report-queue";
    public const string ReportTop5Customers = "top5customers";
    public const string ReportCustomersByCity = "customersbycity";

    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;
    
    private readonly ILogger<RabbitMqClientReportService> _logger;

    public RabbitMqClientReportService(ConnectionFactory connectionFactory, ILogger<RabbitMqClientReportService> logger)
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

        _channel.ExchangeDeclare(ExchangeName, ExchType, durable: true, autoDelete: false);

        _channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, null);


        _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingKey);

        _logger.LogInformation("Report service connected to RabbitMQ...");


        return _channel;

    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();

        _connection?.Close();
        _connection?.Dispose();

        _logger.LogInformation("Report service disconnected from RabbitMQ...");

    }
}