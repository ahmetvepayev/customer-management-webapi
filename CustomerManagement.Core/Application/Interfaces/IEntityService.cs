using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Domain.Interfaces;

namespace CustomerManagement.Core.Application.Interfaces;

public interface IEntityService<TEntity>
    where TEntity : class, IEntity
{
    ObjectResponse<IEnumerable<IResponseDto<TEntity>>> GetAll();
    ObjectResponse<IResponseDto<TEntity>> GetById(int id);
    StatusResponse Add(IRequestDto<TEntity> request);
    StatusResponse Update(int id, IRequestDto<TEntity> request);
    StatusResponse Delete(int id);
}