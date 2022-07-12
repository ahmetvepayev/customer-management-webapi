namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserLoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}