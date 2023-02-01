using Jobs.Identity.Config;
using Jobs.Identity.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
.MinimumLevel.Override("System", LogEventLevel.Warning)
.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
.Enrich.FromLogContext()
.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {Level} {SourceContext}")
.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var migrationAssembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddIdentityServer(opt =>
{
    opt.EmitStaticAudienceClaim = true;
})
.AddTestUsers(InMemoryConfig.TestUsers)
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
.AddDeveloperSigningCredential();

builder.Services.AddRazorPages();

builder.Host.UseSerilog();

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

app.Run();
