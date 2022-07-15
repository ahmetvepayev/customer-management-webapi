using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Domain.Entities;

namespace CustomerManagement.Core.Application.Interfaces.EntityServices;

public interface IReportsListService
{
    ObjectResponse<List<Report>> GetAll();
    Stream GetFileByIdInStream(int id);
}