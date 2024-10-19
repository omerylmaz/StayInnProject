using Auth.API.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Auth.API.Data;

public class AuthDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<IdentityUserRole<string>>()
            .HasData(
            new IdentityUserRole<string>
            {
                RoleId = "1af09578-e6d1-4f63-92f8-42e8a36ba2a1",
                UserId = "310782b3-f29d-44e3-aef1-9417dc9a1337"
            }
            );
        base.OnModelCreating(builder);
    }
}