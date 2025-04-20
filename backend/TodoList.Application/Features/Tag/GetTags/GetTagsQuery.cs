using MediatR;
using TodoList.Application.Common.Requests.Tag.GetTags;

namespace TodoList.Application.Features.Tag.GetTags;

/// <summary>
/// Запрос на получение тегов
/// </summary>
public class GetTagsQuery : IRequest<GetTagsResponse>
{
}