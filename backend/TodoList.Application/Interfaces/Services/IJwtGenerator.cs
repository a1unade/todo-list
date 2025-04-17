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

    /// <summary>
    /// Генерация JWT токена для шагов регистрации
    /// </summary>
    /// <param name="step">Шаг регистрации</param>
    /// <param name="data">Данные о пользователе, полученные при регистрации</param>
    /// <returns>JWT токен для следующего шага регистрации</returns>
    public Task<string> GenerateToken(int step, Dictionary<string, object> data);
}