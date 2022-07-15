using Microsoft.EntityFrameworkCore;
using ReportService.Core.Domain.Entities;

namespace ReportService.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<Report> Reports { get; set; }
    public DbSet<ReportFile> ReportFiles { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CommercialTransaction> CommercialTransactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ReportFile>().HasOne(rf => rf.Report).WithOne(r => r.ReportFile).HasForeignKey<ReportFile>(rf => rf.Id);
    }
}