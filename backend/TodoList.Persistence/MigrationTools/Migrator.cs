using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Interfaces.Contexts;
using TodoList.Persistence.Contexts;

namespace TodoList.Persistence.MigrationTools;

/// <summary>
/// Мигратор для наката миграций и сидов 
/// </summary>
public class Migrator
{
    private readonly ApplicationDbContext _context;
    
    private readonly IDbSeeder _seeder;
    
    private readonly ILogger<Migrator> _logger;

    public Migrator(ApplicationDbContext context, IDbSeeder seeder, ILogger<Migrator> logger)
    {
        _context = context;
        _seeder = seeder;
        _logger = logger;
    }
    
    /// <summary>
    /// Мигратор
    /// </summary>
    public async Task MigrateAsync()
    {
        try
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            await _seeder.SeedAsync(_context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Migrations apply failed {0}", ex.Message);
            throw;
        }
    }
}