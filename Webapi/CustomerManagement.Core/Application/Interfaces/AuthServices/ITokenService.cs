using System.Security.Claims;
using CustomerManagement.Core.Application.Auth;

namespace CustomerManagement.Core.Application.Interfaces.AuthServices;

public interface ITokenService
{
    string GetAccessToken(List<Claim> claims, TokenOptions tokenOptions);
    string GetRefreshToken();
}