using Jobs.Identity.Config;
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

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(InMemoryConfig.IdentityResources)
    .AddInMemoryClients(InMemoryConfig.Clients)
    .AddTestUsers(InMemoryConfig.TestUsers)
    .AddInMemoryApiResources(InMemoryConfig.ApiResources)
    .AddInMemoryApiScopes(InMemoryConfig.ApiScopes)
    .AddDeveloperSigningCredential();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();

app.UseIdentityServer();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
