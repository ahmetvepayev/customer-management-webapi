using ReportService.Core.Domain.Entities;

namespace ReportService.Core.Domain.Interfaces.Repositories;

public interface IReportRepository
{
    void Add(Report report);
}