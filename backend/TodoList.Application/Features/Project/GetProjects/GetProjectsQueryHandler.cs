using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Requests.Project.GetProjects;
using TodoList.Application.Features.Project.UpdateProject;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Application.Features.Project.GetProjects;

/// <summary>
/// Обработчик для <see cref="GetProjectsQuery"/>
/// </summary>
public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, GetProjectsResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="userContext"></param>
    /// <param name="logger"></param>
    public GetProjectsQueryHandler(
        IDbContext dbContext,
        IUserContext userContext,
        ILogger<UpdateProjectCommandHandler> logger)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<GetProjectsResponse> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Projects
            .Where(x => x.UserId == _userContext.UserId)
            .Select(project => new GetProjectsResponseItem
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                StartDate = project.StartDate,
            })
            .ToListAsync(cancellationToken);

        return new GetProjectsResponse
        {
            Entities = result
        };
    }
}