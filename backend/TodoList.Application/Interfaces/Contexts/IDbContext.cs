using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using File = TodoList.Domain.Entities.File;

namespace TodoList.Application.Interfaces.Contexts;

public interface IDbContext
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public DbSet<UserInfo> UserInfos { get; set; }
    
    /// <summary>
    /// Файлы
    /// </summary>
    public DbSet<File> Files { get; set; }
    
    /// <summary>
    /// Роли
    /// </summary>
    public DbSet<Role> Roles { get; set; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    DbSet<T> Set<T>() where T : class;
}