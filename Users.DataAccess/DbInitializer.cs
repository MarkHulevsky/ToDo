using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Users.DataAccess.Entities;

namespace Users.DataAccess;

public static class DbInitializer
{
    public static async Task InitializeDatabaseAsync(IApplicationBuilder app)
    {
        await InitializeAspIdentityDatabaseAsync(app);
    }

    private static async Task InitializeAspIdentityDatabaseAsync(IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        await using var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await context.Database.MigrateAsync();

        if (!context.Users.Any())
        {
            var user = new User
            {
                Email = "markhulevsky@gmail.com",
                UserName = "Mark",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "Qwerty123!");

            await context.SaveChangesAsync();
        }
    }
}