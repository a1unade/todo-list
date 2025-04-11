using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Interfaces.Contexts;
using TodoList.Domain.Entities;

namespace TodoList.Persistence.Seeder;

public class DbSeeder: IDbSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    public DbSeeder(UserManager<User> userManager, RoleManager<Role> roleManager,
        IWebHostEnvironment webHostEnvironment)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async System.Threading.Tasks.Task SeedAsync(IDbContext context, CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(_roleManager, context, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
    
    private static readonly List<Role> _roles = new()
    {
        new Role("Admin"),
        new Role("User")
    };
    
    private static async System.Threading.Tasks.Task SeedRolesAsync(RoleManager<Role> roleManager, IDbContext context,
        CancellationToken cancellationToken)
    {
        foreach (var roleName in _roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName.Name!))
            {
                await roleManager.CreateAsync(new Role { Name = roleName.Name });
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}