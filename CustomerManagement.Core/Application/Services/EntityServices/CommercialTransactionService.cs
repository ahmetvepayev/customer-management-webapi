using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;
using CustomerManagement.Core.Application.DtoValidators;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Utility.Extensions;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Core.Application.Services.EntityServices;

public class CommercialTransactionService : PersistingServiceBase, ICommercialTransactionService
{
    private readonly ILogger<CommercialTransactionService> _logger;
    private readonly IMapper _mapper;
    private readonly ICommercialTransactionRepository _commercialTransactionRepository;
    private readonly ICustomerRepository _customerRepository;

    public CommercialTransactionService(IUnitOfWork unitOfWork, ICommercialTransactionRepository commercialTransactionRepository, IMapper mapper, ICustomerRepository customerRepository, ILogger<CommercialTransactionService> logger)
        : base(unitOfWork)
    {
        _commercialTransactionRepository = commercialTransactionRepository;
        _mapper = mapper;
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public ObjectResponse<List<CommercialTransactionGetResponse>> GetAll()
    {
        int code;
        var rawData = _commercialTransactionRepository.GetAll();

        if (rawData.IsNullOrEmpty())
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
        List<string> errors;
        
        var customer = _customerRepository.GetById(request.CustomerId);
        if (!request.IsValid(customer, out errors))
        {
            code = 400;
            return new StatusResponse(code, errors);
        }

        var addedEntry = _mapper.Map<CommercialTransaction>(request);
        _commercialTransactionRepository.Add(addedEntry);

        try
        {
            if (_unitOfWork.SaveChanges() == 0)
            {
                code = 400;
                errors = new(){
                    "Changes to the database did not persist"
                };
                return new StatusResponse(code, errors);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to add CommercialTransaction");
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
        List<string> errors;

        var updatedEntry = _commercialTransactionRepository.GetById(id);

        if (updatedEntry == null)
        {
            code = 404;
            return new StatusResponse(code);
        }

        var customer = _customerRepository.GetById(request.CustomerId);
        if (!request.IsValid(customer, out errors))
        {
            code = 400;
            return new StatusResponse(code, errors);
        }

        _mapper.Map<CommercialTransactionUpdateRequest, CommercialTransaction>(request, updatedEntry);

        try
        {
            if (_unitOfWork.SaveChanges() == 0)
            {
                code = 400;
                errors = new(){
                    "Changes to the database did not persist"
                };
                return new StatusResponse(code, errors);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to update CommercialTransaction");
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
        List<string> errors;

        var deletedEntry = _commercialTransactionRepository.GetById(id);

        if (deletedEntry == null)
        {
            code = 404;
            return new StatusResponse(code);
        }

        _commercialTransactionRepository.Delete(deletedEntry);

        try
        {
            if (_unitOfWork.SaveChanges() == 0)
            {
                code = 400;
                errors = new(){
                    "Changes to the database did not persist"
                };
                return new StatusResponse(code, errors);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to delete CommercialTransaction");
            code = 500;
            errors = new(){
                "The database responded with an error"
            };
            return new StatusResponse(code, errors);
        }

        code = 200;
        return new StatusResponse(code);
    }

    public ObjectResponse<List<CommercialTransactionGetResponse>> GetAllForCustomer(int customerId)
    {
        int code;
        List<string> errors;

        var customer = _customerRepository.GetByIdIncludeCommercialTransactions(customerId);
        if (customer == null)
        {
            code = 404;
            errors = new(){
                "Customer with the given CustomerId doesn't exist"
            };
            return new ObjectResponse<List<CommercialTransactionGetResponse>>(code, errors);
        }

        if (customer.CommercialTransactions.IsNullOrEmpty())
        {
            code = 404;
            errors = new(){
                "There are no CommercialTransactions for the given Customer"
            };
            return new ObjectResponse<List<CommercialTransactionGetResponse>>(code, errors);
        }

        var data = customer.CommercialTransactions.Select(x => _mapper.Map<CommercialTransactionGetResponse>(x)).ToList();
        code = 200;
        return new ObjectResponse<List<CommercialTransactionGetResponse>>(data, code);
    }
}