namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserGetTokenRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}