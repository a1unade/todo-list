using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Enums;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Requests.Project.GetProjectById;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Application.Features.Project.GetProjectById;

/// <summary>
/// Обработчик для <see cref="GetProjectByIdQuery"/>
/// </summary>
public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, GetProjectByIdResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly ILogger<GetProjectByIdQueryHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="dbContext">Контекст бд</param>
    /// <param name="logger">Логгер</param>
    public GetProjectByIdQueryHandler(
        IUserContext userContext,
        IDbContext dbContext,
        ILogger<GetProjectByIdQueryHandler> logger)
    {
        _userContext = userContext;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<GetProjectByIdResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.Projects
                .Where(project => project.Id == request.Id && _userContext.UserId == project.UserId)
                .Select(project => new GetProjectByIdResponse
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    UserId = project.UserId,
                    Tasks = project.Tasks.Select(task => new GetProjectByIdTaskResponse
                        {
                            Id = task.Id,
                            Title = task.Title,
                            Description = task.Description,
                            CompletedAt = task.CompletedAt,
                            Status = (TaskCompletionStatus)task.Status,
                            Tags = task.Tags
                                .Select(tag => tag.TagId)
                                .ToList(),
                        })
                    .ToList(),
                })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NotFoundException(request.Id);
        }
        catch (Exception e)
        {
            _logger.BeginScope(new Dictionary<string, object>()
            {
                ["Request"] = JsonSerializer.Serialize(request),
                ["Exception"] = e.Message,
                ["StackTrace"] = e.StackTrace ?? string.Empty
            });
            _logger.LogError("Произошла ошибка при получении проекта");
            throw;
        }
    }
}