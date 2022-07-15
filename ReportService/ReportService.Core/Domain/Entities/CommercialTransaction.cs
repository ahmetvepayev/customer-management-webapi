namespace ReportService.Core.Domain.Entities;

public class CommercialTransaction
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Amount { get; set; }

    public Customer Customer { get; set; }
}