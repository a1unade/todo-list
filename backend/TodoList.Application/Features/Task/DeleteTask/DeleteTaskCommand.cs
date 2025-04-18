using MediatR;

namespace TodoList.Application.Features.Task.DeleteTask;

/// <summary>
/// Команда на удаление таска
/// </summary>
public class DeleteTaskCommand : IRequest
{
    /// <summary>
    /// Ид таска
    /// </summary>
    public Guid Id { get; set; }
}