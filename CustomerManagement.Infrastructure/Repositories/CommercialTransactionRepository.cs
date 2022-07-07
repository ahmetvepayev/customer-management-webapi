using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Infrastructure.Database;

namespace CustomerManagement.Infrastructure.Repositories;

public class CommercialTransactionRepository : GenericEntityRepository<CommercialTransaction>, ICommercialTransactionRepository
{
    public CommercialTransactionRepository(AppDbContext context) : base(context)
    {
        
    }
}