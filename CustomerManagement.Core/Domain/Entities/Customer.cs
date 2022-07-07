namespace CustomerManagement.Core.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public byte[] Photo { get; set; }

    public Transaction Transaction { get; set; }
}