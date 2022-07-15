using CustomerManagement.Core.Domain.Entities;

namespace CustomerManagement.Core.Domain.Interfaces.Repositories;

public interface IReportFileRepository
{
    ReportFile GetById(int id);
}