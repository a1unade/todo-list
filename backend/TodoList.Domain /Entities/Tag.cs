using TodoList.Domain.Common;

namespace TodoList.Domain.Entities;

/// <summary>
/// Тег
/// </summary>
public class Tag: BaseEntity
{
    /// <summary>
    /// Текст тега
    /// </summary>
    public string Text { get; set; } = default!;
    
    /// <summary>
    /// Цвет тега
    /// </summary>
    public string Color { get; set; } = default!;
    
    /// <summary>
    /// Задачи, к которым добавили тег (многие-ко-многим)
    /// </summary>
    public ICollection<TaskTag> TaskTags { get; set; } = default!;
}