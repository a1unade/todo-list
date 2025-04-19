using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Enums;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Requests.Task.CreateTask;
using TodoList.Application.Interfaces.Contexts;
using TodoList.Domain.Entities;

namespace TodoList.Application.Features.Task.CreateTask;

/// <summary>
/// Обработчик для <see cref="CreateTaskCommand"/>
/// </summary>
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст бд</param>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="logger">Логгер</param>
    public CreateTaskCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        ILogger<CreateTaskCommandHandler> logger)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<CreateTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.UserId == _userContext.UserId, cancellationToken)
                ?? throw new NotFoundException($"Проект не найден: {request.ProjectId}");

            var tags = await _dbContext.Tags
                .Where(x => request.Tags.Contains(x.Id))
                .ToListAsync(cancellationToken);
            
            var task = new Domain.Entities.Task(
                request.Title,
                request.Description,
                (byte)TaskCompletionStatus.Created,
                project);

            var taskTags = tags.Select(tag => new TaskTag
            {
                Task = task,
                Tag = tag
            });
            
            await _dbContext.TaskTags.AddRangeAsync(taskTags, cancellationToken);
            await _dbContext.Tasks.AddAsync(task, cancellationToken);
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateTaskResponse
            {
                Id = task.Id
            };
        }
        catch (Exception e)
        {
            _logger.BeginScope(new Dictionary<string, object>()
            {
                ["Request"] = JsonSerializer.Serialize(request),
                ["Exception"] = e.Message,
                ["StackTrace"] = e.StackTrace ?? string.Empty
            });
            _logger.LogError("Произошла ошибка при создании задачи");
            throw;
        }
    }
}