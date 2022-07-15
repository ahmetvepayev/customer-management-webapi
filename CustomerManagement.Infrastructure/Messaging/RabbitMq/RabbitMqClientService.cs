using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CustomerManagement.Infrastructure.Messaging.RabbitMq;

public class RabbitMqClientService : IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;

    private readonly ILogger<RabbitMqClientService> _logger;

    public RabbitMqClientService(ConnectionFactory connectionFactory, ILogger<RabbitMqClientService> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;

    }

    public IModel Connect(string exchangeName, string exchangeType, string routingKey, string queueName)
    {
        _connection = _connectionFactory.CreateConnection();


        if (_channel is { IsOpen: true })
        {
            return _channel;
        }

        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchangeName, exchangeType, durable: true, autoDelete: false);

        _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, null);


        _channel.QueueBind(exchange: exchangeName, queue: queueName, routingKey: routingKey);

        _logger.LogInformation("Connected to RabbitMQ...");


        return _channel;

    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();

        _connection?.Close();
        _connection?.Dispose();

        _logger.LogInformation("Disconnected from RabbitMQ...");

    }
}