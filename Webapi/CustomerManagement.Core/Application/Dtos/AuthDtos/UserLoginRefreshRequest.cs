namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserLoginRefreshRequest
{
    public string UserName { get; set; }
    public string RefreshToken { get; set; }
}