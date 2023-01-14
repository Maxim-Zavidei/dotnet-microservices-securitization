using Jobs.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobs.API.Data;

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
