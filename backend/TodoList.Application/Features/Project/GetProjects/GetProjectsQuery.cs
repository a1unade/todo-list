using MediatR;
using TodoList.Application.Common.Requests.Project.GetProjects;

namespace TodoList.Application.Features.Project.GetProjects;

/// <summary>
/// Запрос на получение проектов
/// </summary>
public class GetProjectsQuery : IRequest<GetProjectsResponse>
{
}