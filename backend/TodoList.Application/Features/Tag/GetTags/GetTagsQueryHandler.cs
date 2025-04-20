using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Common.Requests.Tag.GetTags;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Application.Features.Tag.GetTags;

/// <summary>
/// Обработчик для <see cref="GetTagsQuery"/>
/// </summary>
public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, GetTagsResponse>
{
    private readonly IDbContext _dbContext;
    private readonly ILogger<GetTagsQueryHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="dbContext">Контекст бд</param>
    public GetTagsQueryHandler(ILogger<GetTagsQueryHandler> logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<GetTagsResponse> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Tags
                .Select(tag => new GetTagsResponseItem
                {
                    Id = tag.Id,
                    Text = tag.Text,
                    Color = tag.Color
                })
                .ToListAsync(cancellationToken);

            return new GetTagsResponse
            {
                Entities = result
            };
        }
        catch (Exception e)
        {
            _logger.BeginScope(new Dictionary<string, object>()
            {
                ["Exception"] = e.Message,
                ["StackTrace"] = e.StackTrace ?? string.Empty
            });
            _logger.LogError("Произошла ошибка при получении тегов");
            throw;
        }
    }
}