using Microsoft.AspNetCore.Identity;

namespace CustomerManagement.Core.Application.Auth;

public class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}