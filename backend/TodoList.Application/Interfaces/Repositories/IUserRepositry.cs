using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для работы с пользователем
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получить пользователя по Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>User or null</returns>
    Task<User?> FindById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить пользователя по почте
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь либо null</returns>
    Task<User?> FindByEmail(string email, CancellationToken cancellationToken);
}