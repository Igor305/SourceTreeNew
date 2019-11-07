using EducationApp.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EducationApp.BusinessLogicLayer.Services
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData (UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            SeedRoles(roleManager,configuration);
            SeedUsers(userManager,configuration);
        }
        public static void SeedRoles(RoleManager<Role> roleManager, IConfiguration configuration)
        {
            string roleName = configuration.GetSection("Role")["Admin"];
            bool isRoleExists = roleManager.RoleExistsAsync(roleName).Result;
            if (!isRoleExists)
            {
                Role role = new Role();
                role.Name = roleName;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            roleName = configuration.GetSection("Role")["Client"];
            isRoleExists = roleManager.RoleExistsAsync(roleName).Result;
            if (!isRoleExists)
            {
                Role role = new Role();
                role.Name = roleName;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<User> userManager, IConfiguration configuration)
        {
            string emailAdmin = configuration.GetSection("Role")["EmailAdmin"];
            bool isUserFindByEmail = userManager.FindByEmailAsync(emailAdmin).Result == null;
            if (isUserFindByEmail)
            {
                User user = new User();
                user.UserName = emailAdmin;
                user.Email = emailAdmin;

                string passwordAdmin = configuration.GetSection("Role")["PasswordAdmin"];
                IdentityResult result = userManager.CreateAsync(user, passwordAdmin).Result;

                if (result.Succeeded)
                {
                    string roleName = configuration.GetSection("Role")["Admin"];
                    userManager.AddToRoleAsync(user, roleName).Wait();
                }
            }
        }
    }
}