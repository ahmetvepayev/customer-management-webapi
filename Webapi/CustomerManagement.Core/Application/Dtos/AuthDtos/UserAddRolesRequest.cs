namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserAddRolesRequest
{
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
}