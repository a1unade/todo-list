namespace TodoList.Application.Common.Responses.User;

/// <summary>
/// Информация о пользователя
/// </summary>
public class UserInfoResponse : BaseResponse
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string Surname { get; set; } = default!;

    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Никнейм пользователя
    /// </summary>
    public string Nickname { get; set; } = default!;
}