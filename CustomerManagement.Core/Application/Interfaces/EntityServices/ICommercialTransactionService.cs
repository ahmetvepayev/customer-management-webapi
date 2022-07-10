using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;

namespace CustomerManagement.Core.Application.Interfaces.EntityServices;

public interface ICommercialTransactionService
{
    ObjectResponse<List<CommercialTransactionGetResponse>> GetAll();
    ObjectResponse<CommercialTransactionGetResponse> GetById(int id);
    StatusResponse Add(CommercialTransactionAddRequest request);
    StatusResponse Update(int id, CommercialTransactionUpdateRequest request);
    StatusResponse Delete(int id);
}