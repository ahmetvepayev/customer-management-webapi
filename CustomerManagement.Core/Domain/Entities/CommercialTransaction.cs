using CustomerManagement.Core.Domain.Interfaces;

namespace CustomerManagement.Core.Domain.Entities;

public class CommercialTransaction : IEntity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public Customer Customer { get; set; }
}