using TodoList.Domain.Common;

namespace TodoList.Domain.Entities;

/// <summary>
/// Проект
/// </summary>
public class Project: BaseEntity
{
    /// <summary>
    /// Название проекта
    /// </summary>
    public string Title { get; set; } = default!;
    
    /// <summary>
    /// Описание проекта
    /// </summary>
    public string Description { get; set; } = default!;
    
    /// <summary>
    /// Дата создания проекта
    /// </summary>
    public DateOnly StartDate { get; set; }
    
    /// <summary>
    /// Задачи проекта
    /// </summary>
    public ICollection<Task> Tasks { get; set; } = default!;
    
    /// <summary>
    /// Автор проекта (Пользователь, который создал проект)
    /// </summary>
    public User User { get; set; } = default!;
    
    /// <summary>
    /// nav-prop свойство для связи с пользователем
    /// </summary>
    public Guid UserId { get; set; }
}