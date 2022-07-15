using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using CustomerManagement.Core.Application.Interfaces.AuthServices;
using CustomerManagement.Utility.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace CustomerManagement.Core.Application.Auth.Jwt;

public class TokenService : ITokenService
{
    public string GetAccessToken(List<Claim> claims, TokenOptions tokenOptions)
    {
        if (tokenOptions == null)
        {
            return String.Empty;
        }
        claims = claims ?? new();

        var audiences = tokenOptions.Audience;
        var issuer = tokenOptions.Issuer;

        claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        
        var key = GetSymmetricSecurityKey(tokenOptions.SymmetricSecurityKey);

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var expiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenDuration);

        var token = new JwtSecurityToken(
            issuer : issuer,
            expires : expiration,
            notBefore : DateTime.Now,
            claims : claims,
            signingCredentials : credentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return accessToken;
    }

    private SecurityKey GetSymmetricSecurityKey(string key)
    {
        return new SymmetricSecurityKey(key.ToByteArrayUTF8());
    }

    public string GetRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
