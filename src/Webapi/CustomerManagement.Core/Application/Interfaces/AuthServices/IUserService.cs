using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.AuthDtos;

namespace CustomerManagement.Core.Application.Interfaces.AuthServices;

public interface IUserService
{
    Task<ObjectResponse<UserAddResponse>> CreateUserAsync(UserAddRequest request);
    Task<StatusResponse> RemoveUserAsync(UserRemoveRequest request);
    Task<ObjectResponse<UserAddRolesResponse>> AddRolesToUserAsync(UserAddRolesRequest request);
    Task<ObjectResponse<UserRemoveRolesResponse>> RemoveRolesFromUserAsync(UserRemoveRolesRequest request);
    Task<StatusResponse> CreateRoleAsync(RoleAddRequest request);
    Task<ObjectResponse<AuthTokenResponse>> UserLoginAsync(UserLoginRequest request);
    Task<ObjectResponse<AuthTokenResponse>> UserLoginRefreshAsync(UserLoginRefreshRequest request);
}