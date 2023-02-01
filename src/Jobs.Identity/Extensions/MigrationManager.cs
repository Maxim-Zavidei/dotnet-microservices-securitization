using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Jobs.Identity.Config;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Identity.Extensions;

public static class MigrationManager
{
    public static IApplicationBuilder UseMigrationManager(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            try
            {
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in InMemoryConfig.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in InMemoryConfig.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.ApiScopes.Any())
                {
                    foreach (var apiScope in InMemoryConfig.ApiScopes)
                    {
                        context.ApiScopes.Add(apiScope.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.ApiResources.Any())
                {
                    foreach (var resource in InMemoryConfig.ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        return app;
    }
}
