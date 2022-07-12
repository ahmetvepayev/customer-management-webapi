namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserAddRoleRequest
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
}