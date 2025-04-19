using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using File = TodoList.Domain.Entities.File;
using Task = TodoList.Domain.Entities.Task;

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
    /// Проекты
    /// </summary>
    public DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Теги
    /// </summary>
    public DbSet<Tag> Tags { get; set; }

    /// <summary>
    /// Задачи
    /// </summary>
    public DbSet<Task> Tasks { get; set; }

    /// <summary>
    /// Таблица присоединений task + tag
    /// </summary>
    public DbSet<TaskTag> TaskTags { get; set; }
    
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