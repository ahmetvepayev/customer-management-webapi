namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class UserAddRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}