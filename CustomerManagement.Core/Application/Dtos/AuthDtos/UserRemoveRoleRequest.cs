namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserRemoveRoleRequest
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
}