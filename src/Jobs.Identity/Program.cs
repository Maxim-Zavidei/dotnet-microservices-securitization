using Jobs.Identity.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(InMemoryConfig.IdentityResources)
    .AddInMemoryClients(InMemoryConfig.Clients)
    .AddTestUsers(InMemoryConfig.TestUsers)
    .AddDeveloperSigningCredential();

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
