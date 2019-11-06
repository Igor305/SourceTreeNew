using EducationApp.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public static class StartRoleService
    {
        public static void CreateUserRoles(IServiceProvider serviceProvider, IConfiguration _configuration)
        {
            RoleManager<Role> RoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            UserManager<User> UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            Task<bool> roleCheckClient =  RoleManager.RoleExistsAsync(_configuration.GetSection("Role")["Client"]);
            if (!roleCheckClient.Result)
            {
                Role role = new Role();
                role.Name = _configuration.GetSection("Role")["Client"];
                RoleManager.CreateAsync(role);
            }

            Task<bool> roleCheckAdmin = RoleManager.RoleExistsAsync(_configuration.GetSection("Role")["Admin"]);
            if (!roleCheckAdmin.Result)
            {
                Role role = new Role();
                role.Name = _configuration.GetSection("Role")["Admin"];
                RoleManager.CreateAsync(role);
            }
           
           // User user = await UserManager.FindByEmailAsync(_configuration.GetSection("Role")["Admin"]);
           // await UserManager.AddToRoleAsync(user, _configuration.GetSection("Role")["Admin"]);
        }
    }
}
