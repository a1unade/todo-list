using MediatR;
using TodoList.Application.Common.Requests.Tag.GetTagById;

namespace TodoList.Application.Features.Tag.GetTagById;

/// <summary>
/// Запрос на получение тега по ид
/// </summary>
public class GetTagByIdQuery : IRequest<GetTagByIdResponse>
{
    /// <summary>
    /// Ид тега
    /// </summary>
    public Guid Id { get; set; }
}