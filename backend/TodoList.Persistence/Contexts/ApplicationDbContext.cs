using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TodoList.Application.Interfaces.Contexts;
using TodoList.Domain.Entities;
using File = TodoList.Domain.Entities.File;

namespace TodoList.Persistence.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<User, Role,  Guid>, IDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }
    
    public override DbSet<User> Users { get; set; }
    
    public DbSet<UserInfo> UserInfos { get; set; }
    
    public DbSet<File> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Применение конфигураций сущностей из папки Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Extensions.ServiceCollectionExtensions).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}