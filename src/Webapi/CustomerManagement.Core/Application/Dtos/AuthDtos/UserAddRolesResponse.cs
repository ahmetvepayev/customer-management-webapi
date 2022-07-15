namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserAddRolesResponse
{
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
}