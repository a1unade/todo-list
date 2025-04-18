using System.Text.Json;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Requests.Project.CreateProject;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Application.Features.Project.CreateProject;

/// <summary>
/// Обработчик для <see cref="CreateProjectCommand"/>
/// </summary>
public class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, CreateProjectResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private ILogger<CreateProjectCommandHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст бд</param>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="logger">Логгер</param>
    public CreateProjectCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        ILogger<CreateProjectCommandHandler> logger)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<CreateProjectResponse> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            if (string.IsNullOrEmpty(request.Description))
                throw new ValidationException([
                    new ValidationFailure(nameof(request.Description), "Описание обязательно")
                ]);
            
            if (string.IsNullOrEmpty(request.Title))
                throw new ValidationException([
                    new ValidationFailure(nameof(request.Title), "Название обязательно")
                ]);

            if (request.StartDate.Date < DateTime.UtcNow.Date)
                throw new ValidationException([
                    new ValidationFailure(nameof(request.StartDate), "Начало проекта не должно быть позже текущей даты")
                ]);
            
            var currentUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == _userContext.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.User));
            
            var project = new Domain.Entities.Project(
                title: request.Title,
                description: request.Description,
                startDate: DateOnly.FromDateTime(request.StartDate), 
                currentUser);
            
            await _dbContext.Projects.AddAsync(project, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateProjectResponse
            {
                Id = project.Id
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
            _logger.LogError("Произошла ошибка при создании проекта");

            throw;
        }
    }
}