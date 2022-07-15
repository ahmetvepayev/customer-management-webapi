using System.Security.Claims;
using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.AuthDtos;
using CustomerManagement.Core.Application.Interfaces.AuthServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Core.Application.Auth;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public UserService(IConfiguration config, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, ITokenService tokenService, ILogger<UserService> logger)
    {
        _logger = logger;
        _config = config;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<ObjectResponse<UserAddResponse>> CreateUserAsync(UserAddRequest request)
    {
        int code;
        List<string> errors;

        var user = _mapper.Map<AppUser>(request);
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            code = 400;
            errors = new(result.Errors.Select(e => e.Description));
            return new ObjectResponse<UserAddResponse>(code, errors);
        }

        code = 200;
        var response = _mapper.Map<UserAddResponse>(user);

        return new ObjectResponse<UserAddResponse>(response, code);
    }

    public async Task<StatusResponse> RemoveUserAsync(UserRemoveRequest request)
    {
        int code;
        List<string> errors;

        var user = await _userManager.FindByNameAsync(request.UserName);
        
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            code = 400;
            errors = new(result.Errors.Select(e => e.Description));
            return new StatusResponse(code, errors);
        }

        code = 200;
        return new StatusResponse(code);
    }

    public async Task<ObjectResponse<UserAddRolesResponse>> AddRolesToUserAsync(UserAddRolesRequest request)
    {
        int code;
        List<string> errors;

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            code = 404;
            return new ObjectResponse<UserAddRolesResponse>(code);
        }

        try
        {
            var result = await _userManager.AddToRolesAsync(user, request.Roles);

            if (!result.Succeeded)
            {
                code = 400;
                errors = new(result.Errors.Select(e => e.Description));
                return new ObjectResponse<UserAddRolesResponse>(code, errors);
            }
        }
        catch(InvalidOperationException ex)
        {
            _logger.LogError(ex, "Failed to add roles");
            code = 400;
            errors = new(){
                ex.Message
            };
            for(var inner = ex.InnerException; inner != null; inner = inner.InnerException)
            {
                errors.Add(inner.Message);
            }

            return new ObjectResponse<UserAddRolesResponse>(code, errors);
        }

        var data = _mapper.Map<UserAddRolesResponse>(user);
        data.Roles = new(await _userManager.GetRolesAsync(user));

        code = 200;
        return new ObjectResponse<UserAddRolesResponse>(data, code);
    }

    public async Task<ObjectResponse<UserRemoveRolesResponse>> RemoveRolesFromUserAsync(UserRemoveRolesRequest request)
    {
        int code;
        List<string> errors;

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            code = 404;
            return new ObjectResponse<UserRemoveRolesResponse>(code);
        }

        var result = await _userManager.RemoveFromRolesAsync(user, request.Roles);

        if (!result.Succeeded)
        {
            code = 400;
            errors = new(result.Errors.Select(e => e.Description));
            return new ObjectResponse<UserRemoveRolesResponse>(code, errors);
        }

        var data = _mapper.Map<UserRemoveRolesResponse>(user);
        data.Roles = new(await _userManager.GetRolesAsync(user));

        code = 200;
        return new ObjectResponse<UserRemoveRolesResponse>(data, code);
    }

    public async Task<StatusResponse> CreateRoleAsync(RoleAddRequest request)
    {
        int code;
        List<string> errors;
        
        var role = _mapper.Map<AppRole>(request);

        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            code = 400;
            errors = new(result.Errors.Select(e => e.Description));
            return new StatusResponse(code, errors);
        }

        code = 200;
        return new StatusResponse(code);
    }

    public async Task<ObjectResponse<AuthTokenResponse>> UserLoginAsync(UserLoginRequest request)
    {
        int code;
        List<string> errors;

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            code = 400;
            errors = new(){
                "Incorrect UserName and Password combination"
            };
            return new ObjectResponse<AuthTokenResponse>(code, errors);
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            code = 400;
            errors = new(){
                "Incorrect UserName and Password combination"
            };
            return new ObjectResponse<AuthTokenResponse>(code, errors);
        }

        errors = new();
        var data = await GenerateTokenResponse(user, errors);

        if (data == null)
        {
            code = 500;
            return new ObjectResponse<AuthTokenResponse>(code, errors);
        }

        code = 200;
        return new ObjectResponse<AuthTokenResponse>(data, code);
    }

    public async Task<ObjectResponse<AuthTokenResponse>> UserLoginRefreshAsync(UserLoginRefreshRequest request)
    {
        int code;
        List<string> errors;

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            code = 400;
            errors = new(){
                "Incorrect UserName and RefreshToken combination"
            };
            return new ObjectResponse<AuthTokenResponse>(code, errors);
        }
        
        if (user.RefreshToken != request.RefreshToken)
        {
            code = 400;
            errors = new(){
                "Incorrect UserName and RefreshToken combination"
            };
            return new ObjectResponse<AuthTokenResponse>(code, errors);
        }

        if (user.RefreshTokenExpiration < DateTime.UtcNow)
        {
            code = 401;
            errors = new(){
                "RefreshToken expired"
            };
            return new ObjectResponse<AuthTokenResponse>(code, errors);
        }

        errors = new();
        var data = await GenerateTokenResponse(user, errors);

        if (data == null)
        {
            code = 500;
            return new ObjectResponse<AuthTokenResponse>(code, errors);
        }

        code = 200;
        return new ObjectResponse<AuthTokenResponse>(data, code);
    }

    private TokenOptions GetTokenOptions()
    {
        var options = new TokenOptions();
        _config.GetSection("TokenSettings").Bind(options);

        return options;
    }

    private async Task<AuthTokenResponse> GenerateTokenResponse(AppUser user, List<string> errors)
    {
        List<Claim> claims = new(){
            new(ClaimTypes.Name, user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(user);

        if (roles != null)
        {
            claims.AddRange(
                roles.Select(r => new Claim(ClaimTypes.Role, r))
            );
        }

        var tokenOptions = GetTokenOptions();

        var accessToken = _tokenService.GetAccessToken(claims, tokenOptions);

        var refreshToken = _tokenService.GetRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(tokenOptions.RefreshTokenDuration);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = refreshTokenExpiration;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            errors.AddRange(result.Errors.Select(e => e.Description));
            return null;
        }

        var response = new AuthTokenResponse()
        {
            UserName = user.UserName,
            SecurityStamp = user.SecurityStamp,
            ConcurrencyStamp = user.ConcurrencyStamp,
            AccessToken = accessToken,
            AccessTokenExpiration = DateTime.UtcNow.AddMinutes(tokenOptions.AccessTokenDuration),
            RefreshToken = refreshToken,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return response;
    }
}