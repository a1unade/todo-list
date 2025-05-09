namespace TodoList.Application.Interfaces.Contexts;

/// <summary>
/// Seed данных
/// </summary>
public interface IDbSeeder
{
    /// <summary>
    /// Seed данных
    /// </summary>
    /// <param name="context">Контекст базы</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    public Task SeedAsync(IDbContext context, CancellationToken cancellationToken = default);
}