namespace TodoList.Application.Interfaces.Contexts;

/// <summary>
/// Контекст пользователя
/// </summary>
public interface IUserContext
{
    public Guid UserId { get; }
}