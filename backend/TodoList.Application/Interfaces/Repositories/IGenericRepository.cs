using System.Linq.Expressions;

namespace TodoList.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task Add(T entity, CancellationToken cancellationToken);

    IQueryable<T> GetAll();

    IQueryable<T> Get(Expression<Func<T, bool>> predicate);
    
    Task<T?> GetById(Guid id, CancellationToken cancellationToken);

    Task RemoveById(Guid id, CancellationToken cancellationToken);
}