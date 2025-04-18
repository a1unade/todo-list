namespace TodoList.Application.Common.Requests.Project.GetProjects;

/// <summary>
/// Ответ на запрос о получении проектов
/// </summary>
public class GetProjectsResponse
{
    /// <summary>
    /// Проекты
    /// </summary>
    public List<GetProjectsResponseItem> Entities { get; set; }
}