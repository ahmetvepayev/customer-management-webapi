using AutoMapper;
using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Dtos.AuthDtos;

namespace CustomerManagement.Core.Application.Mapping;

public class AppRoleProfile : Profile
{
    public AppRoleProfile()
    {
        CreateMap<RoleAddRequest, AppRole>();
    }
}