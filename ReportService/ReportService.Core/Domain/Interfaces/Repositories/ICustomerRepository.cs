using ReportService.Core.Domain.Entities;

namespace ReportService.Core.Domain.Interfaces.Repositories;

public interface ICustomerRepository
{
    List<Customer> GetTopCustomers(int number);
    List<Customer> GetAll();
}