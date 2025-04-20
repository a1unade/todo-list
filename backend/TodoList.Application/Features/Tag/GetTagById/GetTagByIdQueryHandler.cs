using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Requests.Tag.GetTagById;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Application.Features.Tag.GetTagById;

/// <summary>
/// Обработчик для <see cref="GetTagByIdQuery"/>
/// </summary>
public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, GetTagByIdResponse>
{
    private readonly IDbContext _dbContext;
    private readonly ILogger<GetTagByIdQueryHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="dbContext">Контекст бд</param>
    public GetTagByIdQueryHandler(ILogger<GetTagByIdQueryHandler> logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<GetTagByIdResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.Tags
                .Select(tag => new GetTagByIdResponse
                {
                    Id = tag.Id,
                    Text = tag.Text,
                    Color = tag.Color,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(request.Id);
        }
        catch (Exception e)
        {
            _logger.BeginScope(new Dictionary<string, object>()
            {
                ["Exception"] = e.Message,
                ["StackTrace"] = e.StackTrace ?? string.Empty
            });
            _logger.LogError("Произошла ошибка при получении тега: {tagId}", request.Id);
            throw;
        }
    }
}