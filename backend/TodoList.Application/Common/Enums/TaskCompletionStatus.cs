namespace TodoList.Application.Common.Enums;

/// <summary>
/// Статусы выполнения задачи
/// </summary>
public enum TaskCompletionStatus: byte
{
    /// <summary>
    /// Создана
    /// </summary>
    Created,
    
    /// <summary>
    /// Выполняется
    /// </summary>
    InProgress,
    
    /// <summary>
    /// Проверяется
    /// </summary>
    Checks,
    
    /// <summary>
    /// Выполнена
    /// </summary>
    Completed,
}