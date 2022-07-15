using ReportService.Core.Domain.Entities;
using ReportService.Core.Domain.Interfaces.Repositories;
using ReportService.Infrastructure.Database;

namespace ReportService.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Report report)
    {
        _context.Reports.Add(report);
    }
}