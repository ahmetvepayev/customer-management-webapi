using System.Text;
using CustomerManagement.Core.Application.Interfaces.Messaging;
using CustomerManagement.Infrastructure.Messaging.RabbitMq;
using RabbitMQ.Client;

namespace CustomerManagement.Infrastructure.Messaging;

public class MessageReportService : IMessageReportService
{
    private readonly RabbitMqClientReportService _rabbitMqClientReportService;

    public void CreateCustomersByCityReport()
    {
        var exchangeName = RabbitMqClientWatermarkService.ExchangeName;
        var routingKey = RabbitMqClientWatermarkService.RoutingKey;

        var channel = _rabbitMqClientReportService.Connect();

        var data = Encoding.UTF8.GetBytes(RabbitMqClientReportService.ReportCustomersByCity);

        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: data);
    }

    public void CreateTop5CustomersReport()
    {
        var exchangeName = RabbitMqClientWatermarkService.ExchangeName;
        var routingKey = RabbitMqClientWatermarkService.RoutingKey;

        var channel = _rabbitMqClientReportService.Connect();

        var data = Encoding.UTF8.GetBytes(RabbitMqClientReportService.ReportTop5Customers);

        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: data);
    }
}
