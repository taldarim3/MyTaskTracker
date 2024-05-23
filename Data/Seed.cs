using Microsoft.AspNetCore.Identity;
using MyTaskTracker.Data;
using MyTaskTracker.Models;
using System.Diagnostics;
using System.Net;
using MyTaskTracker.Helpers;

namespace RunGroopWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TaskTrackerDbContext>();

                context.Database.EnsureCreated();

                if (!context.Organizations.Any())
                {
                    context.Organizations.AddRange(new List<Organization>()
                    {
                        new Organization()
                        {
                            Name = "Организация 1",
                            Description = "Тестовая организация 1"
                        }


                    });
                    context.SaveChanges();
                }
            }
        }

        public static async System.Threading.Tasks.Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        Email = adminUserEmail,
                        Surname = "Админский",
                        Name = "Админ",
                        PasswordHash = PassHasher.CalculateMD5Hash("123"),
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        Email = "123@123.ru",
                        Surname = "Тестовый",
                        Name = "Сотрудник",
                        PasswordHash = PassHasher.CalculateMD5Hash("123"),
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}