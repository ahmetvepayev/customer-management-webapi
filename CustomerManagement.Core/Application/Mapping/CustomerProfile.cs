using AutoMapper;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Utility.Extensions;

namespace CustomerManagement.Core.Application.Mapping;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerGetResponse>();
        CreateMap<CustomerAddRequest, Customer>()
            .ForMember(
                dest => dest.Id,
                act => act.Ignore()
            )
            .ForMember(
                dest => dest.CommercialTransactions,
                act => act.Ignore()
            )
            .ForMember(
                dest => dest.Photo,
                opt => opt.MapFrom(src => src.Photo.ToByteArray())
            );
        CreateMap<CustomerUpdateRequest, Customer>()
            .ForMember(
                dest => dest.Id,
                act => act.Ignore()
            )
            .ForMember(
                dest => dest.CommercialTransactions,
                act => act.Ignore()
            )
            .ForMember(
                dest => dest.Photo,
                opt => opt.MapFrom(src => src.Photo.ToByteArray())
            );
    }
}