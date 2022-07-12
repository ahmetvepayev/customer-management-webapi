using CustomerManagement.Api.Extensions;
using CustomerManagement.Core.Application.Dtos.AuthDtos;
using CustomerManagement.Core.Application.Interfaces.AuthServices;
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

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser(UserAddRequest request)
    {
        var response = await _userService.CreateUserAsync(request);
        
        return response.GetActionResult();
    }

    [HttpDelete("users")]
    public async Task<IActionResult> DeleteUser(UserRemoveRequest request)
    {
        var response = await _userService.RemoveUserAsync(request);

        return response.GetActionResult();
    }

    [HttpPost("users/roles")]
    public async Task<IActionResult> AddRolesToUser(UserAddRolesRequest request)
    {
        var response = await _userService.AddRolesToUserAsync(request);

        return response.GetActionResult();
    }

    [HttpDelete("users/roles")]
    public async Task<IActionResult> RemoveRolesFromUser(UserRemoveRolesRequest request)
    {
        var response = await _userService.RemoveRolesFromUserAsync(request);

        return response.GetActionResult();
    }

    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole(RoleAddRequest request)
    {
        var response = await _userService.CreateRoleAsync(request);

        return response.GetActionResult();
    }
}