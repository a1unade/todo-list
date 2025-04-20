namespace TodoList.Application.Common.Requests.Tag.GetTags;

/// <summary>
/// Ответ на запрос о получении тега
/// </summary>
public class GetTagsResponseItem
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