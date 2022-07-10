using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Interfaces;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Core.Domain.Interfaces.Repositories;

namespace CustomerManagement.Core.Application.Services.EntityServices;

public class CustomerService : PersistingServiceBase, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        : base(unitOfWork)
    {
        _customerRepository = customerRepository;
    }

    public ObjectResponse<IEnumerable<IResponseDto<Customer>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public ObjectResponse<IResponseDto<Customer>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Add(IRequestDto<Customer> request)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Update(int id, IRequestDto<Customer> request)
    {
        throw new NotImplementedException();
    }

    public StatusResponse Delete(int id)
    {
        throw new NotImplementedException();
    }
}