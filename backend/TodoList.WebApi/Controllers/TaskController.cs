using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Common.Requests.Task.CreateTask;
using TodoList.Application.Common.Requests.Task.UpdateTask;
using TodoList.Application.Features.Task.CreateTask;
using TodoList.Application.Features.Task.DeleteTask;
using TodoList.Application.Features.Task.UpdateTask;

namespace TodoList.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator">Медиатор</param>
    public TaskController(IMediator mediator)
        => _mediator = mediator;
    
    /// <summary>
    /// Создать задачу
    /// </summary>
    /// <param name="projectId">Ид проекта</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("{projectId}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateTaskResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<CreateTaskResponse> CreateTaskAsync(
        [FromRoute] Guid projectId,
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
        => await _mediator.Send(new CreateTaskCommand
        {
            Title = request.Title,
            Description = request.Description,
            Tags = request.Tags,
            ProjectId = projectId
        }, cancellationToken);
    
    /// <summary>
    /// Изменить задачу
    /// </summary>
    /// <param name="taskId">Ид задачи</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateTaskAsync(
        [FromRoute] Guid taskId,
        [FromBody] UpdateTaskRequest request,
        CancellationToken cancellationToken)
        => await _mediator.Send(new UpdateTaskCommand
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            Tags = request.Tags,
            TaskId = taskId
        }, cancellationToken);
    
    /// <summary>
    /// Удалить задачу
    /// </summary>
    /// <param name="taskId">Ид задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task RemoveTaskAsync(
        [FromRoute] Guid taskId,
        CancellationToken cancellationToken)
        => await _mediator.Send(new DeleteTaskCommand
        {
            Id = taskId
        }, cancellationToken);
}