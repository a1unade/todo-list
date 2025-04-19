using TodoList.Domain.Common;

namespace TodoList.Domain.Entities;

/// <summary>
/// Проект
/// </summary>
public class Project: BaseEntity
{
    public Project(
        string title,
        string description,
        DateOnly startDate,
        User user,
        ICollection<Task>? tasks = null)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        Tasks = tasks ?? new List<Task>();
        User = user;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    private Project()
    {
    }

    /// <summary>
    /// Название проекта
    /// </summary>
    public string Title { get; private set; }
    
    /// <summary>
    /// Описание проекта
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// Дата создания проекта
    /// </summary>
    public DateOnly StartDate { get; set; }
    
    /// <summary>
    /// Задачи проекта
    /// </summary>
    public ICollection<Task> Tasks { get; private set; }
    
    /// <summary>
    /// Автор проекта (Пользователь, который создал проект)
    /// </summary>
    public User User { get; set; } 
    
    /// <summary>
    /// nav-prop свойство для связи с пользователем
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Обновить информацию о проекте
    /// </summary>
    /// <param name="title">Название проекта</param>
    /// <param name="description">Описание проекта</param>
    public void UpdateProjectInfo(string title, string description)
    {
        Title = title;
        Description = description;
    }
}