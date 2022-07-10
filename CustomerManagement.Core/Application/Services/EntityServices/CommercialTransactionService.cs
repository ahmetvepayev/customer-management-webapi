using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;
using CustomerManagement.Core.Application.Interfaces;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Core.Domain.Interfaces.Repositories;

namespace CustomerManagement.Core.Application.Services.EntityServices;

public class CommercialTransactionService : PersistingServiceBase, ICommercialTransactionService
{
    private readonly IMapper _mapper;
    private readonly ICommercialTransactionRepository _commercialTransactionRepository;

    public CommercialTransactionService(IUnitOfWork unitOfWork, ICommercialTransactionRepository commercialTransactionRepository, IMapper mapper)
        : base(unitOfWork)
    {
        _commercialTransactionRepository = commercialTransactionRepository;
        _mapper = mapper;
    }

    public ObjectResponse<IEnumerable<IResponseDto<CommercialTransaction>>> GetAll()
    {
        int code;
        var rawData = _commercialTransactionRepository.GetAll();

        if (rawData == null || !rawData.Any())
        {
            code = 404;
            var errors = new[]{
                "No available data"
            };
            return new ObjectResponse<IEnumerable<IResponseDto<CommercialTransaction>>>(code, errors);
        }

        code = 200;
        var data = rawData.Select(x => _mapper.Map<CommercialTransactionGetResponse>(x));
        return new ObjectResponse<IEnumerable<IResponseDto<CommercialTransaction>>>(data, code);
    }

    public ObjectResponse<IResponseDto<CommercialTransaction>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Update(int id, IRequestDto<CommercialTransaction> request)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Add(IRequestDto<CommercialTransaction> request)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Delete(int id)
    {
        throw new NotImplementedException();
    }
}