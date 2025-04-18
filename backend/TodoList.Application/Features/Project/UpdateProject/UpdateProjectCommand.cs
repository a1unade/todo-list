using MediatR;
using TodoList.Application.Common.Requests.Project.UpdateProject;

namespace TodoList.Application.Features.Project.UpdateProject;

/// <summary>
/// Команда на обновление проекта
/// </summary>
public class UpdateProjectCommand : UpdateProjectRequest, IRequest
{
    /// <summary>
    /// Идентификатор проекта
    /// </summary>
    public Guid Id { get; set; }
}