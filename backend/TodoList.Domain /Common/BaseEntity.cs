namespace TodoList.Domain.Common;

/// <summary>
/// Базовая сущность
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Id сущности
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Когда была создана сущность
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Когда была изменена сущность
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}