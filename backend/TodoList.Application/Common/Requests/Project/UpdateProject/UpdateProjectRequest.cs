namespace TodoList.Application.Common.Requests.Project.UpdateProject;

/// <summary>
/// Запрос на обновление проекта
/// </summary>
public class UpdateProjectRequest
{
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; } = null!;
}