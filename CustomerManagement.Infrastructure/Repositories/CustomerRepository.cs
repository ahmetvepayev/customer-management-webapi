using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Repositories;

public class CustomerRepository : GenericEntityRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {

    }

    public Customer GetByIdIncludeCommercialTransactions(int id)
    {
        return _context.Customers.Include(c => c.CommercialTransactions).FirstOrDefault(c => c.Id == id);
    }
}