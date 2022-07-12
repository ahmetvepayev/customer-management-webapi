using AutoMapper;
using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Dtos.AuthDtos;

namespace CustomerManagement.Core.Application.Mapping;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<UserAddRequest, AppUser>();
    }
}