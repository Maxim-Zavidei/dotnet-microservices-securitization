using Jobs.Identity.Configurations;
using Jobs.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Identity.Data;

public class ApplicationIdentityDbContext : IdentityDbContext<User>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> opt) : base(opt)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleConfiguration());
    }
}
