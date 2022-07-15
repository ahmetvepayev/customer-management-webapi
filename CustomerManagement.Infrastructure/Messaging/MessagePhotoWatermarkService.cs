using CustomerManagement.Core.Application.Interfaces.Messaging;
using CustomerManagement.Infrastructure.Messaging.RabbitMq;
using CustomerManagement.Infrastructure.Messaging.RabbitMq.Constants;
using RabbitMQ.Client;

namespace CustomerManagement.Infrastructure.Messaging;

public class MessagePhotoWatermarkService : IMessagePhotoWatermarkService
{
    private readonly RabbitMqClientService _rabbitMqClientService;

    public MessagePhotoWatermarkService(RabbitMqClientService rabbitMqClientService)
    {
        _rabbitMqClientService = rabbitMqClientService;
    }

    public void AddWatermark(int id)
    {
        var exchangeName = WatermarkConstants.ExchangeName;
        var exchangeType = WatermarkConstants.ExchangeType;
        var routingKey = WatermarkConstants.RoutingKey;
        var queueName = WatermarkConstants.QueueName;

        var channel = _rabbitMqClientService.Connect(exchangeName, exchangeType, routingKey, queueName);

        var data = BitConverter.GetBytes(id);

        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: data);
    }
}
