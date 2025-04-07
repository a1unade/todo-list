namespace TodoList.Application.Common.Requests.User;

public class UserEmailRequest
{
    /// <summary>
    /// Почта пользователя, которую нужно проверить
    /// </summary>
    public string Email { get; set; } = default!;
    
    public UserEmailRequest()
    {
    }

    public UserEmailRequest(UserEmailRequest request)
    {
        Email = request.Email;
    }
}