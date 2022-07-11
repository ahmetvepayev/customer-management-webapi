using System.Linq.Expressions;
using CustomerManagement.Core.Domain.Interfaces;
using CustomerManagement.Infrastructure.Database;

namespace CustomerManagement.Infrastructure.Repositories;

public class GenericEntityRepository<TEntity> : IEntityRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly AppDbContext _context;

    public GenericEntityRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public TEntity GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public List<TEntity> FindAll(Expression<Func<TEntity, bool>> condition)
    {
        return _context.Set<TEntity>().Where(condition).ToList();
    }

    public bool Exists(int id)
    {
        return _context.Set<TEntity>().Any(e => e.Id == id);
    }

    public bool Exists(Expression<Func<TEntity, bool>> condition)
    {
        return _context.Set<TEntity>().Any(condition);
    }
}