using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Application.DtoValidators;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Core.Domain.Interfaces.Repositories;

namespace CustomerManagement.Core.Application.Services.EntityServices;

public class CustomerService : PersistingServiceBase, ICustomerService
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IMapper mapper)

        : base(unitOfWork)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public ObjectResponse<List<CustomerGetResponse>> GetAll()
    {
        int code;
        var rawData = _customerRepository.GetAll();

        if (rawData == null || !rawData.Any())
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

        if (!request.IsValid(out List<string> errors))
        {
            code = 400;
            return new StatusResponse(code, errors);
        }

        var addedEntry = _mapper.Map<Customer>(request);
        _customerRepository.Add(addedEntry);

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

    public StatusResponse Update(int id, CustomerUpdateRequest request)
    {
        int code;

        if (!request.IsValid(out List<string> errors))
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

        updatedEntry = _mapper.Map<Customer>(request);

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
        var deletedEntry = _customerRepository.GetById(id);

        if (deletedEntry == null)
        {
            code = 404;
            return new StatusResponse(code);
        }

        _customerRepository.Delete(deletedEntry);

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