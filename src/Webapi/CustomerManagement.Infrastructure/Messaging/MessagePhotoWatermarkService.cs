using CustomerManagement.Core.Application.Interfaces.Messaging;
using CustomerManagement.Infrastructure.Messaging.RabbitMq;
using RabbitMQ.Client;

namespace CustomerManagement.Infrastructure.Messaging;

public class MessagePhotoWatermarkService : IMessagePhotoWatermarkService
{
    private readonly RabbitMqClientWatermarkService _rabbitMqClientWatermarkService;

    public MessagePhotoWatermarkService(RabbitMqClientWatermarkService rabbitMqClientWatermarkService)
    {
        _rabbitMqClientWatermarkService = rabbitMqClientWatermarkService;
    }

    public void AddWatermark(int id)
    {
        var exchangeName = RabbitMqClientWatermarkService.ExchangeName;
        var routingKey = RabbitMqClientWatermarkService.RoutingKey;

        var channel = _rabbitMqClientWatermarkService.Connect();

        var data = BitConverter.GetBytes(id);

        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: data);
    }
}
