namespace TodoList.Application.Common.Requests.Task.CreateTask;

/// <summary>
/// Ответ на <see cref="CreateTaskRequest"/>
/// </summary>
public class CreateTaskResponse
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; set; }
}