using System.Threading.Channels;
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
    public string Nickname { get; set; } = default!;
    
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
    public Guid? UserInfoId { get; set; }
    
    /// <summary>
    /// Дополнительная Информация о пользователе
    /// </summary>
    public UserInfo UserInfo { get; set; } = default!;
}