namespace TodoList.Application.Common.Requests.Tag.GetTags;

/// <summary>
/// Ответ на запрос о получении тегов
/// </summary>
public class GetTagsResponse
{
    /// <summary>
    /// Теги
    /// </summary>
    public List<GetTagsResponseItem> Entities { get; set; }
}