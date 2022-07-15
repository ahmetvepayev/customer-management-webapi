using CustomerManagement.Core.Domain.Entities;

namespace CustomerManagement.Core.Domain.Interfaces.Repositories;

public interface IReportRepository
{
    List<Report> GetAll();
}