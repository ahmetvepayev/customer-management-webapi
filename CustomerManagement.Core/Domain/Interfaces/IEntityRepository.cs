using System.Linq.Expressions;

namespace CustomerManagement.Core.Domain.Interfaces;

public interface IEntityRepository<TEntity>
    where TEntity : class, IEntity
{
    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);
    void Add(TEntity entity);
    void Delete(TEntity entity);
    IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> condition);
}