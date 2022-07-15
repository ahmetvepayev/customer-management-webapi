namespace ReportService.Core.Domain.Interfaces;

public interface IUnitOfWork
{
    int SaveChanges();
    void BeginTransaction();
    void CommitTransaction();
}