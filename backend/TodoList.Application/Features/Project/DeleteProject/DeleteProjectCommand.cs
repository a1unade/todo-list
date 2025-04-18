using MediatR;

namespace TodoList.Application.Features.Project.DeleteProject;

/// <summary>
/// Команда на удаление проекта
/// </summary>
public class DeleteProjectCommand : IRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор проекта</param>
    public DeleteProjectCommand(Guid id)
        => Id = id;

    /// <summary>
    /// Ид проекта
    /// </summary>
    public Guid Id { get; set; }
}