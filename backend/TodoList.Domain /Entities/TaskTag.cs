namespace TodoList.Domain.Entities;

/// <summary>
/// Дополнительная сущность для связи многие-ко-многим
/// </summary>
public class TaskTag
{
    /// <summary>
    /// nav-prop свойство для связи с задачей
    /// </summary>
    public Guid TaskId { get; set; }
    
    /// <summary>
    /// Задача, к которой относится тег
    /// </summary>
    public Task Task { get; set; } = default!;

    /// <summary>
    /// nav-prop свойство для связи с тегом
    /// </summary>
    public Guid TagId { get; set; }
    
    /// <summary>
    /// Тег, который относится к задаче
    /// </summary>
    public Tag Tag { get; set; } = default!;
}