using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces.Contexts;
using TodoList.Application.Interfaces.Repositories;

namespace TodoList.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IDbContext _context;

    private readonly DbSet<T> _set;

    public GenericRepository(IDbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public async Task Add(T entity, CancellationToken cancellationToken)
    {
        await _set.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> GetAll() => 
        _set.AsNoTracking();

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate) => 
        _set.AsNoTracking().Where(predicate);

    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken) =>
        await _set.FindAsync(keyValues: [id], cancellationToken);
    
    public async Task RemoveById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _set.FindAsync(id, cancellationToken);
        
        if (entity != null)
            _set.Remove(entity);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}