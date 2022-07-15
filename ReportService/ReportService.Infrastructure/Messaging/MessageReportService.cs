using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Core.Application.Interfaces;
using ReportService.Core.Application.Interfaces.Messaging;
using ReportService.Infrastructure.Messaging.RabbitMq;

namespace ReportService.Infrastructure.Messaging;

public class MessageReportService : IMessageReportService
{
    private readonly RabbitMqClientReportService _rabbitMqClientReportService;
    private readonly ICreateReportService _createReportService;
    private readonly ILogger<MessageReportService> _logger;
    private IModel _channel;

    public MessageReportService(RabbitMqClientReportService rabbitMqClientReportService, ICreateReportService createReportService, ILogger<MessageReportService> logger)
    {
        _rabbitMqClientReportService = rabbitMqClientReportService;
        _createReportService = createReportService;
        _logger = logger;

        _channel = _rabbitMqClientReportService.Connect();
    }

    public void StartListen()
    {
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 5, global: false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        _channel.BasicConsume(RabbitMqClientReportService.QueueName, autoAck: false, consumer);

        consumer.Received += CreateReport;
    }

    private Task CreateReport(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            var data = args.Body.ToArray();

            var reportType = Encoding.UTF8.GetString(data);

            switch(reportType)
            {
                case RabbitMqClientReportService.ReportTop5Customers :
                {
                    _createReportService.CreateTop5CustomersReport();
                    break;
                }

                case RabbitMqClientReportService.ReportCustomersByCity :
                {
                    _createReportService.CreateCustomersByCityReport();
                    break;
                }
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while creating report");
        }

        return Task.CompletedTask;
    }
}