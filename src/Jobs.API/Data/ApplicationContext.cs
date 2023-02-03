using Jobs.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Api.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Job> Jobs { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Job>().ToTable("jobs");
    }
}
