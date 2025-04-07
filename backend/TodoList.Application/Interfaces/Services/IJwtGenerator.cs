using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces.Services;

public interface IJwtGenerator
{
    /// <summary>
    /// Генерация JWT токена для авторизации/регистрации
    /// </summary>
    /// <param name="user">Пользователь, для которого генерируем токен</param>
    /// <returns>Токен JWT</returns>
    public Task<string> GenerateToken(User user);
}