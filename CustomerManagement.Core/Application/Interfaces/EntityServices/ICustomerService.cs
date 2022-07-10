using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Domain.Entities;

namespace CustomerManagement.Core.Application.Interfaces.EntityServices;

public interface ICustomerService
{
    ObjectResponse<List<CustomerGetResponse>> GetAll();
    ObjectResponse<CustomerGetResponse> GetById(int id);
    StatusResponse Add(CustomerAddRequest request);
    StatusResponse Update(int id, CustomerUpdateRequest request);
    StatusResponse Delete(int id);
}