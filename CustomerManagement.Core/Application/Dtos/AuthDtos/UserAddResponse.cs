namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserAddResponse
{
    public string UserName { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
}