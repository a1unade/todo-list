using TodoList.Application.Common.Enums;

namespace TodoList.Application.Common.Requests.Task.UpdateTask;

/// <summary>
/// Запрос на обновление задачи
/// </summary>
public class UpdateTaskRequest
{
    /// <summary>
    /// Название задачи
    /// </summary>
    public string Title { get; set; } = null!;
    
    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Статус задачи
    /// </summary>
    public TaskCompletionStatus Status { get; set; }

    /// <summary>
    /// Теги
    /// </summary>
    public List<Guid> Tags { get; set; }
}