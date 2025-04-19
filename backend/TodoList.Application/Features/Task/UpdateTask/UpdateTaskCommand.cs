using MediatR;
using TodoList.Application.Common.Requests.Task.UpdateTask;

namespace TodoList.Application.Features.Task.UpdateTask;

/// <summary>
/// Команда на обновление задачи
/// </summary>
public class UpdateTaskCommand : UpdateTaskRequest, IRequest
{
    /// <summary>
    /// ИД задачи
    /// </summary>
    public Guid TaskId { get; set; }
}