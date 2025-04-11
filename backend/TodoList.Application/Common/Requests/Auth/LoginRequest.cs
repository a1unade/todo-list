using TodoList.Application.Interfaces.Commands;

namespace TodoList.Application.Common.Requests.Auth;

/// <summary>
/// Запрос на вход в аккауент
/// </summary>
public class LoginRequest: IAuthCommand
{
    public LoginRequest()
    {
    }

    public LoginRequest(LoginRequest request)
    {
        Email = request.Email;
        Password = request.Password;
    }

    /// <summary>
    /// Почта пользователя для входа в аккаунт
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Пароль пользователя для входа в аккаунт
    /// </summary>
    public string Password { get; set; } = default!;
}