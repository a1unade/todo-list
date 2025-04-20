using MediatR;
using TodoList.Application.Common.Requests.Project.GetProjectById;

namespace TodoList.Application.Features.Project.GetProjectById;

/// <summary>
/// Запрос на получение проекта
/// </summary>
public class GetProjectByIdQuery : IRequest<GetProjectByIdResponse>
{
    /// <summary>
    /// Ид проекта
    /// </summary>
    public Guid Id { get; set; }
}