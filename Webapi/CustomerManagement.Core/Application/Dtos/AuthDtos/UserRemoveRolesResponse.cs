namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserRemoveRolesResponse
{
    public string UserName { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
    public List<string> Roles { get; set; }
}