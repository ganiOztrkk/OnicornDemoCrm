using Domain.Entities;
using Infastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Middlewares;

public static class CreateFirstUserMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using var scoped = app.Services.CreateScope();
        var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        string[] roleNames = ["admin", "salesperson", "coordinator", "manager", "support"];
        foreach (var roleName in roleNames)
        {
            if (!roleManager.Roles.Any(x => x.Name == roleName))
            {
                var role = new AppRole()
                {
                    Name = roleName
                };
                roleManager.CreateAsync(role).Wait();
            }
        }

        // admin
        CreateUserIfNotExists(userManager, context, "admin@onicorn.com", "Gani", "Admin", "admin", "1");
        
        // manager
        CreateUserIfNotExists(userManager, context, "manager@onicorn.com", "Ayten", "Yönetici", "manager", "1");
        
        // support
        CreateUserIfNotExists(userManager, context, "support@onicorn.com", "Zehra", "Müşteri Destek", "support", "1");
        
        // salespersons
        CreateUserIfNotExists(userManager, context, "salesperson1@onicorn.com", "Ahmet", "Satıcı", "salesperson", "1");
        CreateUserIfNotExists(userManager, context, "salesperson2@onicorn.com", "Mehmet", "Satıcı", "salesperson", "1");
        CreateUserIfNotExists(userManager, context, "salesperson3@onicorn.com", "Ayşe", "Satıcı", "salesperson", "1");
        CreateUserIfNotExists(userManager, context, "salesperson4@onicorn.com", "Fatma", "Satıcı", "salesperson", "1");
        CreateUserIfNotExists(userManager, context, "salesperson5@onicorn.com", "Hasan", "Satıcı", "salesperson", "1");

        // coordinator
        CreateUserIfNotExists(userManager, context, "coordinator@onicorn.com", "Zeynep", "Koordinatör", "coordinator", "1");
    }

    private static void CreateUserIfNotExists(UserManager<AppUser> userManager, ApplicationDbContext context, string email, string name, string lastname, string roleName, string password)
    {
        if (!userManager.Users.Any(x => x.Email == email))
        {
            var appUser = new AppUser()
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                Name = name,
                Lastname = lastname,
            };
            userManager.CreateAsync(appUser, password).Wait();
            AssignRoleToUser(userManager, context, appUser, roleName);
        }
    }

    private static void AssignRoleToUser(UserManager<AppUser> userManager, ApplicationDbContext context, AppUser user, string roleName)
    {
        var roleManager = context.Roles.FirstOrDefault(x => x.Name == roleName);
        var checkUserRole = context.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == roleManager!.Id);
        if (checkUserRole) return;
        if (roleManager != null)
        {
            var appUserRole = new AppUserRole
            {
                UserId = user.Id,
                RoleId = roleManager.Id
            };
            context.UserRoles.Add(appUserRole);
        }
        context.SaveChanges();
    }
}
