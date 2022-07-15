namespace ReportService.Core.Application.Interfaces;

public interface ICreateReportService
{
    void CreateTop5CustomersReport();
    void CreateCustomersByCityReport();
}