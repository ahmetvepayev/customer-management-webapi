using CustomerManagement.Core.Domain.Interfaces;

namespace CustomerManagement.Core.Application.Interfaces;

public interface IResponseDto
{
    
}

public interface IResponseDto<TEntity>
    where TEntity : class, IEntity
{

}