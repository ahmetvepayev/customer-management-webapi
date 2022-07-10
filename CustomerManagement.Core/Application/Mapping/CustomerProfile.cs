using System.Text;
using AutoMapper;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Domain.Entities;

namespace CustomerManagement.Core.Application.Mapping;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerGetResponse>();
        CreateMap<CustomerAddRequest, Customer>()
            .ForMember(
                dest => dest.Phone,
                opt => opt.MapFrom(src => new String(src.Phone.Where(Char.IsDigit).ToArray()))
            )
            .ForMember(
                dest => dest.Photo,
                opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Photo))
            );
        CreateMap<CustomerUpdateRequest, Customer>()
            .ForMember(
                dest => dest.Phone,
                opt => opt.MapFrom(src => new String(src.Phone.Where(Char.IsDigit).ToArray()))
            )
            .ForMember(
                dest => dest.Photo,
                opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Photo))
            );
    }
}