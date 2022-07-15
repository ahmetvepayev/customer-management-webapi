using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Infrastructure.Database;

namespace CustomerManagement.Infrastructure.Repositories;

public class ReportFileRepository : IReportFileRepository
{
    private readonly AppDbContext _context;

    public ReportFile GetById(int id)
    {
        return _context.ReportFiles.Find(id);
    }
}