using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TodoList.Application.Interfaces.Services;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Options;

namespace TodoList.Infrastructure.Services;

/// <summary>
/// Генератор JWT токенов для авторизации/регистрации
/// </summary>
public class JwtGenerator : IJwtGenerator
{
    /// <summary>
    /// Конфигурация для настроек авторизации
    /// </summary>
    private readonly AuthOptions _options;
    
    /// <summary>
    /// Identity User Manager для управления пользователями
    /// </summary>
    private readonly UserManager<User> _userManager;
    
    public JwtGenerator(IConfiguration configuration, UserManager<User> userManager)
    {
        _userManager = userManager;
        _options = configuration.GetSection("JwtSettings").Get<AuthOptions>()!;
    }
    
    /// <summary>
    /// Генерация JWT токена для авторизации/регистрации
    /// </summary>
    /// <param name="user">Пользователь, для которого генерируем JWT токен</param>
    /// <returns>JWT токен пользователя</returns>
    public async Task<string> GenerateToken(User user)
    {
        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        
        Claim[] claims = 
        {
            new ("Id", user.Id.ToString()),
            new ("Email", user.Email!),
            new ("Name", user.Nickname),
            new ("Role", isAdmin ? "Admin" : "User")
        };

        var issuer = _options.Issuer;
        var audience = _options.Audience;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: credentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}