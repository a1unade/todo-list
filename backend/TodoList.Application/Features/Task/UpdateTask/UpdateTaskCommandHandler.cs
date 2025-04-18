using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Interfaces.Contexts;
using TodoList.Domain.Entities;

namespace TodoList.Application.Features.Task.UpdateTask;

/// <summary>
/// Обработчик для <see cref="UpdateTaskCommand"/>
/// </summary>
public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateTaskCommandHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="dbContext">Контекст бд</param>
    /// <param name="logger">Логгер</param>
    public UpdateTaskCommandHandler(
        IUserContext userContext,
        IDbContext dbContext,
        ILogger<UpdateTaskCommandHandler> logger)
    {
        _userContext = userContext;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async System.Threading.Tasks.Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            
            var task = await _dbContext.Tasks
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => 
                    x.Project.UserId == _userContext.UserId &&
                    x.Id == request.TaskId, cancellationToken)
                ?? throw new NotFoundException("Задача не найдена");
            
            var tags = await _dbContext.Tags
                .Where(x => request.Tags.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var tagsToDelete = task.Tags
                .Where(x => tags.All(y => y.Id != x.TagId))
                .ToList();

            var tagsToAdd = request.Tags
                .Where(x => task.Tags.All(y => y.TagId != x))
                .ToList();

            var newTags = tagsToAdd.Select(tag => new TaskTag
            {
                TaskId = task.Id,
                TagId = tag,
            })
            .ToList();
            
            task.Title = request.Title;
            task.Description = request.Description;
            task.Tags = newTags;
            task.Status = (byte)request.Status;
            
            _dbContext.TaskTags.RemoveRange(tagsToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.BeginScope(new Dictionary<string, object>()
            {
                ["Request"] = JsonSerializer.Serialize(request),
                ["Exception"] = e.Message,
                ["StackTrace"] = e.StackTrace ?? string.Empty
            });
            _logger.LogError("Произошла ошибка при изменении задачи");
            throw;
        }
    }
}