namespace TodoList.Application.Common.Requests.Email;

public class CodeCheckRequest
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// Код подтверждения почты
    /// </summary>
    public string Email { get; set; } = default!;
    
    public CodeCheckRequest()
    {
    }

    public CodeCheckRequest(CodeCheckRequest request)
    {
        Code = request.Code;
        Email = request.Email;
    }
}