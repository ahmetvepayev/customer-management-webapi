using Microsoft.EntityFrameworkCore.Storage;
using ReportService.Core.Domain.Interfaces;

namespace ReportService.Infrastructure.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        if (_transaction == null)
        {
            return;
        }
        
        _transaction.Commit();
    }
}