using demo.WebApiDotNetCore8.Data;
using demo.WebApiDotNetCore8.Models;
using Microsoft.AspNetCore.Identity;

namespace demo.WebApiDotNetCore8
{
   /// <summary>
   /// PM> dotnet run seeddata
   /// </summary>
   public static class DataSeederStandAlone
   {
      public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
      {
         using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
         {

            var _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Seed Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { UserRoles.Admin, UserRoles.User };

            foreach (var role in roles)
            {
               if (!await roleManager.RoleExistsAsync(role))
               {
                  await roleManager.CreateAsync(new IdentityRole(role));
               }
            }
            Console.WriteLine("Seeding Roles Complete. ");


            const string UserEmail = "user@example.com";
            const string AdminEmail = "admin@example.com";

            // Seed Identity User
            if (await _userManager.FindByNameAsync(UserEmail) == null)
            {
               var user = new IdentityUser { UserName = UserEmail, Email = UserEmail, EmailConfirmed = true, PhoneNumber = "2014567890", PhoneNumberConfirmed = true };
               await _userManager.CreateAsync(user, "User@123!*!");
               await _userManager.AddToRoleAsync(user, UserRoles.User);

               Console.WriteLine("Seeding Defaults Users Complete.");
            }

            // Seed Identity Admin
            if (await _userManager.FindByNameAsync("admin") == null)
            {
               var user = new IdentityUser { UserName = AdminEmail, Email = AdminEmail, EmailConfirmed = true, PhoneNumber = "7044567890", PhoneNumberConfirmed = true };
               await _userManager.CreateAsync(user, "Admin@123!*!");
               await _userManager.AddToRoleAsync(user, UserRoles.Admin);

               Console.WriteLine("Seeding Defaults Admins Complete.");
            }

            // Seed demo Court Locations
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context.Database.EnsureCreated();


            if (!context.Courts.Any())
            {
               context.Courts.AddRange(new List<Court>()
               {
                  new Court
                  {
                     CourtType = "Indoor",
                     NumbOfCourts = 6,
                     Title = "Pickleball Court 1",
                     StreetAddress = "123 Main St",
                     City = "Anytown",
                     StateAbrev = "NJ",
                     ZipCode = "07436",
                     Latitude = "40.1234",
                     Longitude = "-73.1234",
                  },
                  new Court
                  {
                     CourtType = "Outdoor",
                     NumbOfCourts = 2,
                     Title = "Pickleball Court 2",
                     StreetAddress = "456 Elm St",
                     City = "Othertown",
                     StateAbrev = "NC",
                     ZipCode = "28104",
                     Latitude = "40.1234",
                     Longitude = "-73.1234",
                  }
               }); ;
               context.SaveChanges();
            }
         }
      }
   }
}
