using MediatR;
using TodoList.Application.Common.Requests.Task.CreateTask;

namespace TodoList.Application.Features.Task.CreateTask;

/// <summary>
/// Команда на создание задачи
/// </summary>
public class CreateTaskCommand : CreateTaskRequest, IRequest<CreateTaskResponse>
{
    /// <summary>
    /// Идентификатор проекта
    /// </summary>
    public Guid ProjectId { get; set; }
}