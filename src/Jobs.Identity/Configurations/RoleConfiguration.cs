using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobs.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "a0f3813f-c3c6-4966-a0d5-a9c90f5384ff"
            },
            new IdentityRole
            {
                Name = "Guest",
                NormalizedName = "GUEST",
                Id = "c6256701-096b-4efd-b6be-20a540702bc0"
            }
        );
    }
}
