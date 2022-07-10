using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;
using CustomerManagement.Core.Application.DtoValidators;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Core.Domain.Interfaces.Repositories;

namespace CustomerManagement.Core.Application.Services.EntityServices;

public class CommercialTransactionService : PersistingServiceBase, ICommercialTransactionService
{
    private readonly IMapper _mapper;
    private readonly ICommercialTransactionRepository _commercialTransactionRepository;
    private readonly ICustomerRepository _customerRepository;

    public CommercialTransactionService(IUnitOfWork unitOfWork, ICommercialTransactionRepository commercialTransactionRepository, IMapper mapper, ICustomerRepository customerRepository)
        : base(unitOfWork)
    {
        _commercialTransactionRepository = commercialTransactionRepository;
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public ObjectResponse<List<CommercialTransactionGetResponse>> GetAll()
    {
        int code;
        var rawData = _commercialTransactionRepository.GetAll();

        if (rawData == null || !rawData.Any())
        {
            code = 404;
            return new ObjectResponse<List<CommercialTransactionGetResponse>>(code);
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
            return new ObjectResponse<CommercialTransactionGetResponse>(code);
        }

        code = 200;
        var data = _mapper.Map<CommercialTransactionGetResponse>(rawData);
        var response = new ObjectResponse<CommercialTransactionGetResponse>(data, code);
        return response;
    }

    public StatusResponse Add(CommercialTransactionAddRequest request)
    {
        int code;
        
        var customer = _customerRepository.GetById(request.CustomerId);
        if (!request.IsValid(customer, out List<string> errors))
        {
            code = 400;
            return new StatusResponse(code, errors);
        }

        var addedEntry = _mapper.Map<CommercialTransaction>(request);
        _commercialTransactionRepository.Add(addedEntry);

        if (_unitOfWork.SaveChanges() == 0)
        {
            code = 500;
            errors = new(){
                "The database responded with an error"
            };
            return new StatusResponse(code, errors);
        }

        code = 200;
        return new StatusResponse(code);
    }

    public StatusResponse Update(int id, CommercialTransactionUpdateRequest request)
    {
        int code;

        var customer = _customerRepository.GetById(request.CustomerId);
        if (!request.IsValid(customer, out List<string> errors))
        {
            code = 400;
            return new StatusResponse(code, errors);
        }

        var updatedEntry = _commercialTransactionRepository.GetById(id);

        if (updatedEntry == null)
        {
            code = 404;
            return new StatusResponse(code);
        }

        updatedEntry = _mapper.Map<CommercialTransaction>(request);

        if (_unitOfWork.SaveChanges() == 0)
        {
            code = 500;
            errors = new(){
                "The database responded with an error"
            };
            return new StatusResponse(code, errors);
        }

        code = 200;
        return new StatusResponse(code);
    }

    public StatusResponse Delete(int id)
    {
        int code;
        var deletedEntry = _commercialTransactionRepository.GetById(id);

        if (deletedEntry == null)
        {
            code = 404;
            return new StatusResponse(code);
        }

        _commercialTransactionRepository.Delete(deletedEntry);

        if (_unitOfWork.SaveChanges() == 0)
        {
            code = 500;
            List<string> errors = new(){
                "The database responded with an error"
            };
            return new StatusResponse(code, errors);
        }

        code = 200;
        return new StatusResponse(code);
    }
}