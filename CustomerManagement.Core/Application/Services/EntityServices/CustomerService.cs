using AutoMapper;
using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
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
            var errors = new List<string>(){
                "No available data"
            };
            return new ObjectResponse<List<CustomerGetResponse>>(code, errors);
        }

        code = 200;
        var data = rawData.Select(x => _mapper.Map<CustomerGetResponse>(x)).ToList();
        var response = new ObjectResponse<List<CustomerGetResponse>>(data, code);
        return response;
    }

    public ObjectResponse<CustomerGetResponse> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Add(CustomerAddRequest request)
    {
        int code;
        var addRequest = (CustomerAddRequest)request;
        var addedEntry = _mapper.Map<Customer>(addRequest);
        _customerRepository.Add(addedEntry);

        _unitOfWork.SaveChanges();
        code = 200;

        return new StatusResponse(code);
    }

    public StatusResponse Update(int id, CustomerUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Delete(int id)
    {
        throw new NotImplementedException();
    }
}