using WatermarkService.Core.Domain.Entities;

namespace WatermarkService.Core.Domain.Interfaces;

public interface ICustomerRepository
{
    Customer GetById(int id);
}