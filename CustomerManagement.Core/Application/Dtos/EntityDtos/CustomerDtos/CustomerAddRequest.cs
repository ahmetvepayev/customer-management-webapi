using CustomerManagement.Core.Application.Interfaces;
using CustomerManagement.Core.Domain.Entities;

namespace CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;

public class CustomerAddRequest : IRequestDto<Customer>
{
    public string Firstname { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public byte[]? Photo { get; set; }
}