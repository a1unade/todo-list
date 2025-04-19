using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Application.Features.Project.UpdateProject;

/// <summary>
/// Обработчик для <see cref="UpdateProjectCommand"/>
/// </summary>
public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="logger"></param>
    /// <param name="userContext"></param>
    public UpdateProjectCommandHandler(
        IDbContext dbContext,
        ILogger<UpdateProjectCommandHandler> logger,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _logger = logger;
        _userContext = userContext;
    }

    public async System.Threading.Tasks.Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _userContext.UserId, cancellationToken)
                ?? throw new NotFoundException(request.Id);
            
            project.UpdateProjectInfo(request.Title, request.Description);
            
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
            _logger.LogError("Произошла ошибка при изменении проекта");
            throw;
        }
    }
}