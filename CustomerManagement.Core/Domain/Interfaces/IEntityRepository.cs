using System.Linq.Expressions;

namespace CustomerManagement.Core.Domain.Interfaces;

public interface IEntityRepository<TEntity>
    where TEntity : class, IEntity
{
    List<TEntity> GetAll();
    TEntity GetById(int id);
    void Add(TEntity entity);
    void Delete(TEntity entity);
    List<TEntity> FindAll(Expression<Func<TEntity, bool>> condition);
    bool Exists(int id);
    bool Exists(Expression<Func<TEntity, bool>> condition);
}