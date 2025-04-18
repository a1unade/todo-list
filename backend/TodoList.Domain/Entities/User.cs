using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TodoList.Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Отображаемое имя
    /// </summary>
    [MaxLength(60)]
    public required string Nickname { get; set; }
    
    /// <summary>
    /// nav-prop Файл
    /// </summary>
    public Guid? AvatarId { get; set; }

    /// <summary>
    /// Аватаркa
    /// </summary>
    public File? AvatarUrl { get; set; } 
    
    /// <summary>
    /// nav-prop для информации о пользователе
    /// </summary>
    public Guid UserInfoId { get; set; }
    
    /// <summary>
    /// Дополнительная Информация о пользователе
    /// </summary>
    public UserInfo UserInfo { get; set; } = default!;
    
    /// <summary>
    /// Проекты пользователя
    /// </summary>
    public ICollection<Project> Projects { get; set; } = default!;
}