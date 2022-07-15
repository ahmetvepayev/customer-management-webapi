using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.EntityRules;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Database;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CommercialTransaction> CommercialTransactions { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<ReportFile> ReportFiles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>().Property(e => e.Firstname).HasMaxLength(CustomerRules.FirstNameMaxLength);
        modelBuilder.Entity<Customer>().Property(e => e.LastName).HasMaxLength(CustomerRules.LastNameMaxLength);
        modelBuilder.Entity<Customer>().Property(e => e.Email).HasMaxLength(CustomerRules.EmailMaxLength);
        modelBuilder.Entity<Customer>().Property(e => e.Phone).HasMaxLength(CustomerRules.PhoneMaxLength);
        modelBuilder.Entity<Customer>().Property(e => e.City).HasMaxLength(CustomerRules.CityMaxLength);

        modelBuilder.Entity<Customer>().HasIndex(e => e.Phone).IsUnique();

        modelBuilder.Entity<CommercialTransaction>().Property(e => e.Description)
            .HasMaxLength(CommercialTransactionRules.DescriptionMaxLength);
        modelBuilder.Entity<CommercialTransaction>().Property(e => e.Amount)
            .HasPrecision(CommercialTransactionRules.AmountMaxLength, CommercialTransactionRules.AmountPrecision);

        modelBuilder.Entity<ReportFile>().HasOne(rf => rf.Report).WithOne(r => r.ReportFile).HasForeignKey<ReportFile>(rf => rf.Id);
    }
}