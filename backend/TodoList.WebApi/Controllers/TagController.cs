using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Common.Requests.Tag.GetTagById;
using TodoList.Application.Common.Requests.Tag.GetTags;
using TodoList.Application.Features.Tag.GetTagById;
using TodoList.Application.Features.Tag.GetTags;

namespace TodoList.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator">Медиатор</param>
    public TagController(IMediator mediator)
        => _mediator = mediator;
    
    /// <summary>
    /// Получить список тегов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список тегов</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTagsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<GetTagsResponse> GetTagsAsync(CancellationToken cancellationToken)
        => await _mediator.Send(new GetTagsQuery(), cancellationToken);

    /// <summary>
    /// Получить тег по ИД
    /// </summary>
    /// <param name="tagId">ИД тега</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Тег</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTagByIdResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{tagId}")]
    public async Task<GetTagByIdResponse> GetTagByIdAsync([FromRoute] Guid tagId, CancellationToken cancellationToken)
        => await _mediator.Send(new GetTagByIdQuery
        {
            Id = tagId
        }, cancellationToken);
}