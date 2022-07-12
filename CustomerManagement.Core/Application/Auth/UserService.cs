using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.AuthDtos;
using CustomerManagement.Core.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CustomerManagement.Core.Application.Auth;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;

    public UserService(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<StatusResponse> CreateUserAsync(UserAddRequest request)
    {
        int code;
        List<string> errors;

        var user = _mapper.Map<AppUser>(request);
        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            code = 400;
            errors = new(){
                "UserName or Password not acceptable"
            };
            return new StatusResponse(code, errors);
        }

        code = 200;
        return new StatusResponse(code);
    }

    public Task<StatusResponse> RemoveUserAsync(UserRemoveRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<StatusResponse> AddRoleToUserAsync(UserAddRoleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<StatusResponse> RemoveRoleFromUserAsync(UserRemoveRoleRequest request)
    {
        throw new NotImplementedException();
    }

    public ObjectResponse<AuthTokenResponse> GetToken(UserGetTokenRequest request)
    {
        throw new NotImplementedException();
    }

    public ObjectResponse<AuthTokenResponse> GetToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}