using Microsoft.EntityFrameworkCore;
using ReportService.Core.Domain.Entities;
using ReportService.Core.Domain.Interfaces.Repositories;
using ReportService.Infrastructure.Database;

namespace ReportService.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Customer> GetAll()
    {
        return _context.Customers.ToList();
    }

    public List<Customer> GetTopCustomers(int number)
    {
        return _context.Customers
            .Include(c => c.CommercialTransactions)
            .OrderByDescending(c => c.CommercialTransactions.Count())
            .Take(number)
            .ToList();
    }
}