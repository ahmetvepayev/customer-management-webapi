using CustomerManagement.Api.Extensions;
using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Dtos.AuthDtos;
using CustomerManagement.Core.Application.Interfaces.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser(UserAddRequest request)
    {
        var response = await _userService.CreateUserAsync(request);
        
        return response.GetActionResult();
    }

    [Authorize(Roles = AppRoleConstants.Admin)]
    [HttpDelete("users")]
    public async Task<IActionResult> DeleteUser(UserRemoveRequest request)
    {
        var response = await _userService.RemoveUserAsync(request);

        return response.GetActionResult();
    }

    [Authorize(Roles = AppRoleConstants.Admin)]
    [HttpPost("users/roles")]
    public async Task<IActionResult> AddRolesToUser(UserAddRolesRequest request)
    {
        var response = await _userService.AddRolesToUserAsync(request);

        return response.GetActionResult();
    }

    [Authorize(Roles = AppRoleConstants.Admin)]
    [HttpDelete("users/roles")]
    public async Task<IActionResult> RemoveRolesFromUser(UserRemoveRolesRequest request)
    {
        var response = await _userService.RemoveRolesFromUserAsync(request);

        return response.GetActionResult();
    }

    [Authorize(Roles = AppRoleConstants.Admin)]
    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole(RoleAddRequest request)
    {
        var response = await _userService.CreateRoleAsync(request);

        return response.GetActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> UserLogin(UserLoginRequest request)
    {
        var response = await _userService.UserLoginAsync(request);

        return response.GetActionResult();
    }

    [HttpPost("loginrefresh")]
    public async Task<IActionResult> UserLoginRefresh(UserLoginRefreshRequest request)
    {
        var response = await _userService.UserLoginRefreshAsync(request);
        
        return response.GetActionResult();
    }
}