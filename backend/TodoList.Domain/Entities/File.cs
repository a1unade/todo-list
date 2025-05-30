using TodoList.Domain.Common;

namespace TodoList.Domain.Entities;

/// <summary>
/// Файл
/// </summary>
public class File : BaseEntity
{
    /// <summary>
    /// Размер
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Тип файла
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// Путь в S3
    /// </summary>
    public string Path { get; set; } = default!;

    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; } = default!;

    /// <summary>
    /// Название бакета
    /// </summary>
    public string BucketName { get; set; } = default!;
}