using AutoMapper;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;
using CustomerManagement.Core.Domain.Entities;

namespace CustomerManagement.Core.Application.Mapping;

public class CommercialTransactionProfile : Profile
{
    public CommercialTransactionProfile()
    {
        CreateMap<CommercialTransaction, CommercialTransactionGetResponse>();
        CreateMap<CommercialTransactionAddRequest, CommercialTransaction>();
    }
}