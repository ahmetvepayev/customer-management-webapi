using CustomerManagement.Core.Domain.Interfaces;

namespace CustomerManagement.Core.Application.Interfaces;

public interface IRequestDto
{
    
}

public interface IRequestDto<TEntity>
    where TEntity : class, IEntity
{

}