using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Infrastructure.Database;

namespace CustomerManagement.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public List<Report> GetAll()
    {
        return _context.Reports.ToList();
    }
}
