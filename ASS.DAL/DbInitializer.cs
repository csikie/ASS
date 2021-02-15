using ASS.Common.Enums;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ASS.DAL
{
    public static class DbInitializer
    {
        private static ASSContext context;

        public static void Initialize(IServiceProvider serviceProvider, UserManager<User> userManager)
        {
            context = serviceProvider.GetRequiredService<ASSContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                CreateSystemData(userManager);
            }


        }

        private async static void CreateSystemData(UserManager<User> userManager)
        {
            foreach (Role item in Enum.GetValues(typeof(Role)))
            {
                context.Roles.Add(new IdentityRole<int>() { Name = item.ToString(), NormalizedName = item.ToString() });
            }

            User user = new User("admin","Teszt Elek");
            IdentityResult res = await userManager.CreateAsync(user, "Ab1234");
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Role.Admin.ToString());
            }

            User user2 = new User("student", "Teszt Elek");
            IdentityResult res2 = await userManager.CreateAsync(user2, "Ab1234");
            if (res2.Succeeded)
            {
                await userManager.AddToRoleAsync(user2, Role.Student.ToString());
            }

            User user3 = new User("poora", "Poór Artúr");
            IdentityResult res3 = await userManager.CreateAsync(user3, "Ab1234");
            if (res3.Succeeded)
            {
                await userManager.AddToRoleAsync(user3, Role.Teacher.ToString());
            }

            User user4 = new User("b0vh4g", "Csiki Erik Gergely");
            IdentityResult res4 = await userManager.CreateAsync(user4, "Ab1234");
            if (res4.Succeeded)
            {
                await userManager.AddToRoleAsync(user4, Role.Instructor.ToString());
            }

            User user5 = new User("bozo_i", "Bozó István");
            IdentityResult res5 = await userManager.CreateAsync(user5, "Ab1234");
            if (res5.Succeeded)
            {
                await userManager.AddToRoleAsync(user5, Role.Teacher.ToString());
            }
        }
    }
}
