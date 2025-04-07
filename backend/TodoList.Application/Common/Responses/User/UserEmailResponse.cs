namespace TodoList.Application.Common.Responses.User;

public class UserEmailResponse : BaseResponse
{
    /// <summary>
    /// Новый ли пользователь
    /// </summary>
    public bool NewUser { get; set; }
    
    /// <summary>
    /// Требуется ли подтверждение почты пользователя
    /// </summary>
    public bool Confirmation { get; set; }
}