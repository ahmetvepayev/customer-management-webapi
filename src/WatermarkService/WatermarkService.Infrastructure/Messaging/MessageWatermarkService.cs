using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WatermarkService.Core.Application.Interfaces;
using WatermarkService.Core.Application.Interfaces.Messaging;
using WatermarkService.Infrastructure.Messaging.RabbitMq;

namespace WatermarkService.Infrastructure.Messaging;

public class MessageWatermarkService : IMessageWatermarkService
{
    private readonly RabbitMqClientWatermarkService _rabbitMqClientWatermarkService;
    private readonly IImageService _imageService;
    private readonly ILogger<MessageWatermarkService> _logger;
    private IModel _channel;

    public MessageWatermarkService(RabbitMqClientWatermarkService rabbitMqClientWatermarkService, IImageService imageService, ILogger<MessageWatermarkService> logger)
    {
        _rabbitMqClientWatermarkService = rabbitMqClientWatermarkService;
        _imageService = imageService;
        _logger = logger;

        _channel = _rabbitMqClientWatermarkService.Connect();
    }

    public void StartListen()
    {
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 5, global: false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        _channel.BasicConsume(RabbitMqClientWatermarkService.QueueName, autoAck: false, consumer);

        consumer.Received += AddWatermark;
    }

    private Task AddWatermark(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            var data = args.Body.ToArray();

            var id = BitConverter.ToInt32(data);

            _imageService.AddWatermarkToPhoto(id);

            _logger.LogInformation($"Added watermark to image for Id: {id}");

            _channel.BasicAck(args.DeliveryTag, false);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while adding watermark to image");
        }

        return Task.CompletedTask;
    }
}