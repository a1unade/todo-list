using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Common;

namespace TodoList.Domain.Entities;

/// <summary>
/// Задача для проекта
/// </summary>
public class Task: BaseEntity
{
    /// <summary>
    /// Название задачи
    /// </summary>
    public string Title { get; set; } = default!;
    
    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Description { get; set; } = default!;
    
    /// <summary>
    /// Дата создания задачи
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Дата завершения задачи
    /// nullable
    /// </summary>
    public DateTime? CompletedAt { get; set; }
    
    /// <summary>
    /// Статус выполнения задачи
    /// </summary>
    public byte Status { get; set; }
    
    /// <summary>
    /// Теги, добавленные к задаче
    /// </summary>
    public ICollection<TaskTag> Tags { get; set; } = default!;
    
    /// <summary>
    /// Проект, к которому относится задача
    /// </summary>
    public Project Project { get; set; } = default!;
    
    /// <summary>
    /// nav-prop свойство для связи с проектом 
    /// </summary>
    public Guid ProjectId { get; set; }
}