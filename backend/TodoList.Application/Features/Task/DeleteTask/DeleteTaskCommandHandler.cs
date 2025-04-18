using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Application.Features.Task.DeleteTask;

/// <summary>
/// Обработчик для <see cref="DeleteTaskCommand"/>
/// </summary>
public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly ILogger<DeleteTaskCommandHandler> _logger;

    public DeleteTaskCommandHandler(
        IUserContext userContext,
        IDbContext dbContext,
        ILogger<DeleteTaskCommandHandler> logger)
    {
        _userContext = userContext;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async System.Threading.Tasks.Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _dbContext.Tasks
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => 
                    x.Project.UserId == _userContext.UserId &&
                    x.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("Задача не найдена");
            
            _dbContext.Tasks.Remove(task);
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
            _logger.LogError("Произошла ошибка при удалении задачи");
            throw;
        }
    }
}