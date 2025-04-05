using Microsoft.AspNetCore.Identity;

namespace TodoList.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public Role(string roleName) : base(roleName)
    {
    }

    public Role()
    {
    }
}