namespace TodoList.Application.Common.Requests.Email;

public class EmailConfirmRequest 
{
    /// <summary>
    /// Id пользователя, для которого подтверждаем почту
    /// </summary>
    public Guid Id { get; set; } 
    
    public EmailConfirmRequest()
    {
    }
    
    public EmailConfirmRequest(EmailConfirmRequest request)
    {
        Id = request.Id;
    }
}