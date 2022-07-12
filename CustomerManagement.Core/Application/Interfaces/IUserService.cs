using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.AuthDtos;

namespace CustomerManagement.Core.Application.Interfaces;

public interface IUserService
{
    Task<StatusResponse> CreateUserAsync(UserAddRequest request);
    Task<StatusResponse> RemoveUserAsync(UserRemoveRequest request);
    Task<StatusResponse> AddRoleToUserAsync(UserAddRoleRequest request);
    Task<StatusResponse> RemoveRoleFromUserAsync(UserRemoveRoleRequest request);
    ObjectResponse<AuthTokenResponse> GetToken(UserGetTokenRequest request);
    ObjectResponse<AuthTokenResponse> GetToken(string refreshToken);
}