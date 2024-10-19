using Auth.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.API.Data.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData
                (
                new AppRole { Id = "2ec2844b-e8df-4f25-981f-5a7d4bec2b21", Name = "Host", NormalizedName = "HOST" },
                new AppRole { Id = "93395636-9fee-4b34-bd58-ea7dfbd60842", Name = "Guest", NormalizedName = "GUEST" },
                new AppRole { Id = "1af09578-e6d1-4f63-92f8-42e8a36ba2a1", Name = "Admin", NormalizedName = "ADMIN" }
                );
        }
    }
}
