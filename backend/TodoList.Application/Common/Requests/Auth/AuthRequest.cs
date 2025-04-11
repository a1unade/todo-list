using TodoList.Application.Interfaces.Commands;

namespace TodoList.Application.Common.Requests.Auth;

/// <summary>
/// Запрос на регистрацию
/// </summary>
public class AuthRequest: IAuthCommand
{
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; } = default!;
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; } = default!;
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string Surname { get; set; } = default!;
    
    /// <summary>
    /// Дата рождения пользователя
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// Пол пользователя
    /// </summary>
    public string Gender { get; set; } = default!;

    /// <summary>
    /// Страна пользователя
    /// </summary>
    public string Country { get; set; } = default!;
    
    public AuthRequest()
    {
    }
    
    public AuthRequest(AuthRequest request)
    {
        Password = request.Password;
        Email = request.Email;
        Name = request.Name;
        Surname = request.Surname;
        DateOfBirth = request.DateOfBirth;
        Gender = request.Gender;
        Country = request.Country;
    }
}