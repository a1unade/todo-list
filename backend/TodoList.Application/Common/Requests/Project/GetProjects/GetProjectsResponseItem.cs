namespace TodoList.Application.Common.Requests.Project.GetProjects;

/// <summary>
/// Элемент списка для <see cref="GetProjectsResponse"/>
/// </summary>
public class GetProjectsResponseItem
{
    /// <summary>
    /// Ид проекта
    /// </summary>
    public Guid Id { get; set; }
    
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
    public DateOnly StartDate { get; set; }
}