namespace CustomerManagement.Infrastructure.Messaging.RabbitMq.Constants;

public static class WatermarkConstants
{
    public const string ExchangeName = "WatermarkDirectExchange";
    public const string ExchangeType = "direct";
    public const string RoutingKey = "watermark-route";
    public const string QueueName = "watermark-queue";
}