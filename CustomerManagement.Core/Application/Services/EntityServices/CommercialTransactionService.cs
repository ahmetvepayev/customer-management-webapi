using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;
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

    public ObjectResponse<List<CommercialTransactionGetResponse>> GetAll()
    {
        int code;
        var rawData = _commercialTransactionRepository.GetAll();

        if (rawData == null || !rawData.Any())
        {
            code = 404;
            var errors = new[]{
                "No available data"
            };
            return new ObjectResponse<List<CommercialTransactionGetResponse>>(code, errors);
        }

        code = 200;
        var data = rawData.Select(x => _mapper.Map<CommercialTransactionGetResponse>(x)).ToList();
        var response = new ObjectResponse<List<CommercialTransactionGetResponse>>(data, code);
        return response;
    }

    public ObjectResponse<CommercialTransactionGetResponse> GetById(int id)
    {
        int code;
        var rawData = _commercialTransactionRepository.GetById(id);

        if (rawData == null)
        {
            code = 404;
            var errors = new[]{
                "No available data"
            };
            return new ObjectResponse<CommercialTransactionGetResponse>(code, errors);
        }

        code = 200;
        var data = _mapper.Map<CommercialTransactionGetResponse>(rawData);
        var response = new ObjectResponse<CommercialTransactionGetResponse>(data, code);
        return response;
    }

    public StatusResponse Add(CommercialTransactionAddRequest request)
    {
        int code;
        var addRequest = (CommercialTransactionAddRequest)request;
        var addedEntry = _mapper.Map<CommercialTransaction>(addRequest);
        _commercialTransactionRepository.Add(addedEntry);

        _unitOfWork.SaveChanges();
        code = 200;

        return new StatusResponse(code);
    }

    public StatusResponse Update(int id, CommercialTransactionUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Delete(int id)
    {
        throw new NotImplementedException();
    }
}