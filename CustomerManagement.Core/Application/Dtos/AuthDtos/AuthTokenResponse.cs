namespace CustomerManagement.Core.Application.Dtos.AuthDtos;

public class AuthTokenResponse
{
    public string UserName { get; set; }
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}