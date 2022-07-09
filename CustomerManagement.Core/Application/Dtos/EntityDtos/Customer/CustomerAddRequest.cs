namespace CustomerManagement.Core.Application.Dtos.EntityDtos.Customer;

public class CustomerAddRequest
{
    public string Firstname { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public byte[]? Photo { get; set; }
}