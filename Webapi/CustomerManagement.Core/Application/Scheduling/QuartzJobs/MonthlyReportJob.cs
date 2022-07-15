using CustomerManagement.Core.Application.Interfaces.Messaging;
using Quartz;

namespace CustomerManagement.Core.Application.Scheduling.QuartzJobs;

public class MonthlyReportJob : IJob
{
    private readonly IMessageReportService _messageReportService;

    public MonthlyReportJob(IMessageReportService messageReportService)
    {
        _messageReportService = messageReportService;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _messageReportService.CreateCustomersByCityReport();

        return Task.CompletedTask;
    }
}
