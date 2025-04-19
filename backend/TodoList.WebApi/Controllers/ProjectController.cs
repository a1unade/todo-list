using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Common.Requests.Project.CreateProject;
using TodoList.Application.Common.Requests.Project.GetProjects;
using TodoList.Application.Common.Requests.Project.UpdateProject;
using TodoList.Application.Features.Project.CreateProject;
using TodoList.Application.Features.Project.DeleteProject;
using TodoList.Application.Features.Project.GetProjects;
using TodoList.Application.Features.Project.UpdateProject;

namespace TodoList.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator">Медиатор</param>
    public ProjectController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Получить список проектов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список проектов</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProjectsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetProjectsResponse> GetProjectsAsync(CancellationToken cancellationToken)
        => await _mediator.Send(new GetProjectsQuery(), cancellationToken);
    
    /// <summary>
    /// Создать проект
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateProjectResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request, CancellationToken cancellationToken)
        => await _mediator.Send(new CreateProjectCommand
        {
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
        }, cancellationToken);

    /// <summary>
    /// Удалить проект
    /// </summary>
    /// <param name="projectId">Идентификатор проекта</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task DeleteProjectAsync([FromRoute] Guid projectId, CancellationToken cancellationToken)
        => await _mediator.Send(new DeleteProjectCommand(projectId), cancellationToken);

    /// <summary>
    /// Изменить проект
    /// </summary>
    /// <param name="projectId">Идентификатор проекта</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateProjectAsync(
        [FromRoute] Guid projectId,
        [FromBody] UpdateProjectRequest request,
        CancellationToken cancellationToken)
        => await _mediator.Send(new UpdateProjectCommand
        {
            Title = request.Title,
            Description = request.Description,
            Id = projectId
        }, cancellationToken);
}