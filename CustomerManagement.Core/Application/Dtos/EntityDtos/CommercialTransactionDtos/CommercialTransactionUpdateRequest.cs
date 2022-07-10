using CustomerManagement.Core.Application.Interfaces;
using CustomerManagement.Core.Domain.Entities;

namespace CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;

public class CommercialTransactionUpdateRequest
{
    public int? CustomerId { get; set; }
    public string Description { get; set; }
    public decimal? Amount { get; set; }
    public DateTime Date { get; set; }
}