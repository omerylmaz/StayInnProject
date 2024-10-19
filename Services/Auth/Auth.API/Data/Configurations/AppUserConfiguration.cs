using Auth.API.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Auth.API.Data.Configurations
{
    public class AppUserConfiguration(/*IHostEnvironment environment*/) : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //if (environment.IsDevelopment())
            //{
            builder.HasKey(x => x.Id);

            builder.HasData(new List<AppUser>()
                {
                    new AppUser()
                        {
                            Id = "310782b3-f29d-44e3-aef1-9417dc9a1337",
                            FullName = "Admin test",
                            Email = "admin@stayinn.com",
                            NormalizedEmail = "ADMIN@STAYINN.COM",
                            UserName = "admin",
                            NormalizedUserName = "ADMIN",
                            EmailConfirmed = true,
                            PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin123")
                        }
                });

            //builder.HasData(
            //new IdentityUserRole<string>
            //{
            //    RoleId = "1af09578-e6d1-4f63-92f8-42e8a36ba2a1",
            //    UserId = "310782b3-f29d-44e3-aef1-9417dc9a1337"
            //}
            //);
        }
    }

}

