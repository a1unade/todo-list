using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using TodoList.Application.Interfaces.Contexts;
using TodoList.Application.Interfaces.Repositories;
using TodoList.Domain.Entities;

namespace TodoList.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;

    public UserRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task<User?> FindById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(x => x.UserInfo)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        return user;
    }

    public async Task<User?> FindByEmail(string email, CancellationToken cancellationToken) => 
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
}