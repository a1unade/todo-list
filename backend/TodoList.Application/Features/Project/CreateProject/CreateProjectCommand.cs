using MediatR;
using TodoList.Application.Common.Requests.Project.CreateProject;

namespace TodoList.Application.Features.Project.CreateProject;

/// <summary>
/// Команда на создание проекта
/// </summary>
public class CreateProjectCommand
    : CreateProjectRequest, IRequest<CreateProjectResponse>
{
}