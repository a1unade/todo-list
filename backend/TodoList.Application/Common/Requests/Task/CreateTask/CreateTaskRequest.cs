namespace TodoList.Application.Common.Requests.Task.CreateTask;

/// <summary>
/// Запрос на создание задачи
/// </summary>
public class CreateTaskRequest
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
    /// Теги
    /// </summary>
    public List<Guid> Tags { get; set; }
}