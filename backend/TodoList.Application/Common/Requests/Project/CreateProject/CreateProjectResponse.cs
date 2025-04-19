namespace TodoList.Application.Common.Requests.Project.CreateProject;

/// <summary>
/// Ответ на <see cref="CreateProjectRequest"/>
/// </summary>
public class CreateProjectResponse
{
    /// <summary>
    /// Ид проекта
    /// </summary>
    public Guid Id { get; set; }
}