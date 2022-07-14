using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Application.DtoValidators;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Utility.Extensions;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Core.Application.Services.EntityServices;

public class CustomerService : PersistingServiceBase, ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IMapper mapper, ILogger<CustomerService> logger)

        : base(unitOfWork)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public ObjectResponse<List<CustomerGetResponse>> GetAll()
    {
        int code;
        var rawData = _customerRepository.GetAll();

        if (rawData.IsNullOrEmpty())
        {
            code = 404;
            return new ObjectResponse<List<CustomerGetResponse>>(code);
        }

        code = 200;
        var data = rawData.Select(x => _mapper.Map<CustomerGetResponse>(x)).ToList();
        var response = new ObjectResponse<List<CustomerGetResponse>>(data, code);
        return response;
    }

    public ObjectResponse<CustomerGetResponse> GetById(int id)
    {
        int code;
        var rawData = _customerRepository.GetById(id);

        if (rawData == null)
        {
            code = 404;
            return new ObjectResponse<CustomerGetResponse>(code);
        }

        code = 200;
        var data = _mapper.Map<CustomerGetResponse>(rawData);
        var response = new ObjectResponse<CustomerGetResponse>(data, code);
        return response;
    }

    public StatusResponse Add(CustomerAddRequest request)
    {
        int code;
        List<string> errors;

        // Validator also extracts digits from the Phone string
        if (!request.IsValid(out errors))
        {
            code = 400;
            return new StatusResponse(code, errors);
        }

        if (_customerRepository.Exists(e => e.Phone == request.Phone))
        {
            code = 400;
            errors = new(){
                "Phones are required to be unique. There is already a Customer with the given Phone"
            };
            return new StatusResponse(code, errors);
        }

        var addedEntry = _mapper.Map<Customer>(request);
        _customerRepository.Add(addedEntry);

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
            _logger.LogError(ex, "Failed to add Customer");
            code = 500;
            errors = new(){
                "The database responded with an error"
            };
            return new StatusResponse(code, errors);
        }
        

        code = 200;
        return new StatusResponse(code);
    }

    public StatusResponse Update(int id, CustomerUpdateRequest request)
    {
        int code;
        List<string> errors;

        // Validator also extracts digits from the Phone string
        if (!request.IsValid(out errors))
        {
            code = 400;
            return new StatusResponse(code, errors);
        }

        var updatedEntry = _customerRepository.GetById(id);

        if (updatedEntry == null)
        {
            code = 404;
            return new StatusResponse(code);
        }

        // Ensure that the updated phone is unique
        if (_customerRepository.Exists(c => c.Phone == request.Phone && c.Id != id))
        {
            code = 400;
            errors = new(){
                "Phones are required to be unique. There is already another Customer with the given Phone"
            };
            return new StatusResponse(code, errors);
        }

        _mapper.Map<CustomerUpdateRequest, Customer>(request, updatedEntry);

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
            _logger.LogError(ex, "Failed to update Customer");
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
        var deletedEntry = _customerRepository.GetById(id);

        if (deletedEntry == null)
        {
            code = 404;
            return new StatusResponse(code);
        }

        _customerRepository.Delete(deletedEntry);

        try
        {
            if (_unitOfWork.SaveChanges() == 0)
            {
                code = 500;
                List<string> errors = new(){
                    "The database responded with an error"
                };
                return new StatusResponse(code, errors);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to delete CommercialTransaction");
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