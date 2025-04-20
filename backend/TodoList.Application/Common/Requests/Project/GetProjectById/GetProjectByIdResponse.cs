namespace TodoList.Application.Common.Requests.Project.GetProjectById;

/// <summary>
/// Ответ на запрос о получении проекта
/// </summary>
public class GetProjectByIdResponse
{
    /// <summary>
    /// ИД проекта
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название проекта
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Описание проекта
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Дата создания проекта
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Кто создал проект
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Задачи проекта
    /// </summary>
    public List<GetProjectByIdTaskResponse> Tasks { get; set; }
}