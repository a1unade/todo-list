using Microsoft.AspNetCore.Http;
using TodoList.Application.Interfaces.Contexts;

namespace TodoList.Infrastructure.Contexts;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="httpContextAccessor">Http акссесор</param>
    public UserContext(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;
    
    public Guid UserId => Guid.TryParse(
        _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value, out Guid userId)
        ? userId
        : Guid.Empty;
}