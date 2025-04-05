namespace TodoList.Application.Interfaces.Services;

public interface IEmailService
{
    /// <summary>
    /// Отправка письма
    /// </summary>
    /// <param name="email">Почта юзера</param>
    /// <param name="subject">Заголовок</param>
    /// <param name="messageBody">Сообщение</param>
    /// <returns></returns>
    Task SendEmailAsync(string email, string subject, string messageBody);
    
    /// <summary>
    /// Генерация кода подтверждения почты
    /// </summary>
    /// <returns>Код подтверждения почты</returns>
    string GenerateRandomCode();
}