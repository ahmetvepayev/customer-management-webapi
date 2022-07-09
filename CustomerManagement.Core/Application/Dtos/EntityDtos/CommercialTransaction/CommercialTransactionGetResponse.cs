namespace CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransaction;

public class CommercialTransactionGetResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}