using EntityLayer.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Identity
{
    public class SeedIdentity
    {
        public static void Seed3(UserManager<UserAdmin> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = new UserAdmin()
            {
                UserName = "admin",
                Email = "gky.klkn@gmail.com",
                FullName = "Admin Gökay"

            };
            if (userManager.FindByNameAsync("Admin Gökay").Result == null)
            {

                var identtiyResult = userManager.CreateAsync(user, "admin*|1907").Result;
            }
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "Admin"
                };
                var identityResult = roleManager.CreateAsync(role).Result;
                var result = userManager.AddToRoleAsync(user, role.Name).Result;
            }
        }
    }
}
