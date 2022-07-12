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
}