using System.Reflection;
using Jobs.Identity.Data;
using Jobs.Identity.Extensions;
using Jobs.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

// Log.Logger = new LoggerConfiguration()
// .MinimumLevel.Debug()
// .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
// .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
// .MinimumLevel.Override("System", LogEventLevel.Warning)
// .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
// .Enrich.FromLogContext()
// .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {Level} {SourceContext}")
// .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<ApplicationIdentityDbContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("IdentityStoreDefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>()
.AddEntityFrameworkStores<ApplicationIdentityDbContext>()
.AddDefaultTokenProviders();

var migrationAssembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddIdentityServer(opt =>
{
    opt.EmitStaticAudienceClaim = true;
})
.AddConfigurationStore(opt =>
{
    opt.ConfigureDbContext = e => e.UseNpgsql(
        builder.Configuration.GetConnectionString("ConfigurationStoreDefaultConnection"),
        sql => sql.MigrationsAssembly(migrationAssembly)
    );
})
.AddOperationalStore(opt =>
{
    opt.ConfigureDbContext = e => e.UseNpgsql(
        builder.Configuration.GetConnectionString("OperationalStoreDefaultConnection"),
        sql => sql.MigrationsAssembly(migrationAssembly)
    );
})
.AddAspNetIdentity<User>()
.AddDeveloperSigningCredential();

builder.Services.AddRazorPages();

// builder.Host.UseSerilog();

var app = builder.Build();

app.UseMigrationManager();

app.UseRouting();
app.UseStaticFiles();

app.UseIdentityServer();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.SeedIdentityStore();

app.Run();
