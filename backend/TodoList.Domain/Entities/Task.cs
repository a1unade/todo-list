using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Common;

namespace TodoList.Domain.Entities;

/// <summary>
/// Задача для проекта
/// </summary>
public class Task : BaseEntity
{
    public Task(
        string title,
        string description,
        byte status,
        Project project)
    {
        Title = title;
        Description = description;
        Status = status;
        Project = project;
    }

    private Task()
    {
    }

    /// <summary>
    /// Название задачи
    /// </summary>
    public string Title { get; set; } = null!;
    
    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Description { get; set; } = null!;
    
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
    public List<TaskTag> Tags { get; set; } = null!;
    
    /// <summary>
    /// Проект, к которому относится задача
    /// </summary>
    public Project Project { get; set; } = null!;
    
    /// <summary>
    /// nav-prop свойство для связи с проектом 
    /// </summary>
    public Guid ProjectId { get; set; }
}