using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Application.Features.Project.DeleteProject;

/// <summary>
/// Обработчик для <see cref="DeleteProjectCommand"/>
/// </summary>
public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly ILogger<DeleteProjectCommandHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст бд</param>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="logger">Логгер</param>
    public DeleteProjectCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        ILogger<DeleteProjectCommandHandler> logger)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _logger = logger;
    }

    public async System.Threading.Tasks.Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            
            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _userContext.UserId, cancellationToken)
                ?? throw new NotFoundException($"Проект не найден с идентификатором {request.Id}");
            
            _dbContext.Projects.Remove(project);
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
            _logger.LogError("Произошла ошибка при удалении проекта");
            throw;
        }
    }
}