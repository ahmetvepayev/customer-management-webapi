using WatermarkService.Core.Domain.Entities;
using WatermarkService.Core.Domain.Interfaces;
using WatermarkService.Infrastructure.Database;

namespace WatermarkService.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public Customer GetById(int id)
    {
        return _context.Customers.Find(id);
    }
}