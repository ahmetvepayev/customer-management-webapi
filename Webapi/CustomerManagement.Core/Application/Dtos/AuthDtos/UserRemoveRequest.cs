namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserRemoveRequest
{
    public string UserName { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
}