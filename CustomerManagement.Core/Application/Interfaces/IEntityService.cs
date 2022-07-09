using System.Linq.Expressions;
using CustomerManagement.Core.Domain.Interfaces;

namespace CustomerManagement.Core.Application.Interfaces;

public interface IEntityService<TEntity>
    where TEntity : class, IEntity
{
    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);
    void Add(TEntity entity);
    void Delete(int id);
    IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> condition);
}