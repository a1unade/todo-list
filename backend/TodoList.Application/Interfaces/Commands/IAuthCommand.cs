namespace TodoList.Application.Interfaces.Commands;

/// <summary>
/// Шаблон команд авторизации для валидации
/// </summary>
public interface IAuthCommand
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
}