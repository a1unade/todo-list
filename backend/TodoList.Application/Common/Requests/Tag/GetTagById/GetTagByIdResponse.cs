namespace TodoList.Application.Common.Requests.Tag.GetTagById;

/// <summary>
/// Ответ на запрос тега по ид
/// </summary>
public class GetTagByIdResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Текст тега
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Цвет тега
    /// </summary>
    public string Color { get; set; }
}