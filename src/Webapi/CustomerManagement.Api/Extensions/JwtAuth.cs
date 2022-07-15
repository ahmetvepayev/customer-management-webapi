using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Utility.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CustomerManagement.Api.Extensions;

public static class JwtAuth
{
    public static IServiceCollection AddTokenAuth(this IServiceCollection services, ConfigurationManager config)
    {
        var tokenOptions = new TokenOptions();
        config.GetSection("TokenSettings").Bind(tokenOptions);
        
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts => {
            opts.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience[0],
                IssuerSigningKey = new SymmetricSecurityKey(tokenOptions.SymmetricSecurityKey.ToByteArrayUTF8()),

                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}