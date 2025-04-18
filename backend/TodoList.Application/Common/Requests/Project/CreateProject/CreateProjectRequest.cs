namespace TodoList.Application.Common.Requests.Project.CreateProject;

/// <summary>
/// Запрос на создание проекта
/// </summary>
public class CreateProjectRequest
{
    /// <summary>
    /// Название проекта
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание проекта
    /// </summary>
    public string Description { get; set; } = null!;
    
    /// <summary>
    /// Дата создания проекта
    /// </summary>
    public DateTime StartDate { get; set; }
}