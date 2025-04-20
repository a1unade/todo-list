using TodoList.Application.Common.Enums;

namespace TodoList.Application.Common.Requests.Project.GetProjectById;

/// <summary>
/// Задачи для <see cref="GetProjectByIdResponse"/>
/// </summary>
public class GetProjectByIdTaskResponse
{
    /// <summary>
    /// Ид задачи
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название задачи
    /// </summary>
    public string Title { get; set; } = null!;
    
    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Description { get; set; } = null!;
    
    /// <summary>
    /// Дата завершения задачи
    /// nullable
    /// </summary>
    public DateTime? CompletedAt { get; set; }
    
    /// <summary>
    /// Статус выполнения задачи
    /// </summary>
    public TaskCompletionStatus Status { get; set; }

    /// <summary>
    /// Теги
    /// </summary>
    public List<Guid> Tags { get; set; }
}