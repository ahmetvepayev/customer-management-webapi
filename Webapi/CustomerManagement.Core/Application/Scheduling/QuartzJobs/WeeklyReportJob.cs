using CustomerManagement.Core.Application.Interfaces.Messaging;
using Quartz;

namespace CustomerManagement.Core.Application.Scheduling.QuartzJobs;

public class WeeklyReportJob : IJob
{
    private readonly IMessageReportService _messageReportService;

    public WeeklyReportJob(IMessageReportService messageReportService)
    {
        _messageReportService = messageReportService;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _messageReportService.CreateTop5CustomersReport();

        return Task.CompletedTask;
    }
}
