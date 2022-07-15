namespace CustomerManagement.Core.Application.Interfaces.Messaging;

public interface IMessageReportService
{
    void CreateTop5CustomersReport();
    void CreateCustomersByCityReport();
}