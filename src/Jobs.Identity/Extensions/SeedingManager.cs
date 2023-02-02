using System.Security.Claims;
using IdentityModel;
using Jobs.Identity.Data;
using Jobs.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Identity.Extensions;

public static class SeedingManager
{
    public static IApplicationBuilder SeedIdentityStore(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (context.Database.GetPendingMigrations().Count() != 0)
            {
                Console.WriteLine("[SeedingManager]: Warning can't seed data if IdentityStore database is not fully migrated.");
                return app;
            }

            CreateUser(context, userManager, "Bob", "Bobsen", "Bobsen Street 123", "123Password!", "Admin", "bob@gmail.com");
            CreateUser(context, userManager, "Alice", "Alicesen", "Alicesen Street 123", "123Password!", "Guest", "alice@gmail.com");

            context.SaveChanges();
        }
        return app;
    }

    private static void CreateUser(ApplicationIdentityDbContext scope, UserManager<User> userManager, string firstName, string lastName, string address, string password, string role, string email)
    {
        var user = userManager.FindByEmailAsync(email).Result;
        if (user == null)
        {
            user = new User
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Address = address
            };
            var result = userManager.CreateAsync(user, password).Result;
            CheckResult(result);

            result = userManager.AddToRoleAsync(user, role).Result;
            CheckResult(result);

            result = userManager.AddClaimsAsync(user, new Claim[]
            {
                new Claim(JwtClaimTypes.GivenName, user.FirstName),
                new Claim(JwtClaimTypes.FamilyName, user.LastName),
                new Claim(JwtClaimTypes.Role, role),
                new Claim(JwtClaimTypes.Address, user.Address),
            }).Result;
            CheckResult(result);
        }
    }

    private static void CheckResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
    }
}
