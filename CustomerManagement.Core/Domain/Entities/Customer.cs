using CustomerManagement.Core.Domain.Interfaces;

namespace CustomerManagement.Core.Domain.Entities;

public class Customer : IEntity
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public byte[] Photo { get; set; }

    public List<CommercialTransaction> CommercialTransactions { get; set; } = new();
}