using CoursesPlatform.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Controllers
{
    public class RoleInitializer
    {
        private List<string> _initialRoles =  new List<string> { "admin", "employee", "teacher"};
        private readonly IConfiguration _configuration;

        public RoleInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = _configuration["AdminAccess:Default:Login"];
            string adminPassword = _configuration["AdminAccess:Default:Password"];

            string employeeEmail = _configuration["EmployeeAccess:Default:Login"];
            string employeePassword = _configuration["EmployeeAccess:Default:Password"];

            await CreateRoles(roleManager, _initialRoles);
            await CreateDefaultEntity(userManager, adminEmail, adminPassword, _initialRoles[0]);
            await CreateDefaultEntity(userManager, employeeEmail, employeePassword, _initialRoles[1]);
        }

        private async Task CreateDefaultEntity(UserManager<User> userManager, string entityEmail, string adminPassword, string roleName)
        {
            if (await userManager.FindByNameAsync(entityEmail) == null)
            {
                User entity = new User { Email = entityEmail, UserName = entityEmail };
                IdentityResult result = await userManager.CreateAsync(entity, adminPassword);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(entity);
                await userManager.ConfirmEmailAsync(entity, token);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(entity, roleName);
                }
            }
        }

        private async Task CreateRoles(RoleManager<IdentityRole> roleManager, List<string> roles)
        {
            foreach (var role in roles)
            {
                await CreateRole(roleManager, role);
            }
        }

        private async Task CreateRole(RoleManager<IdentityRole> roleManager, string name)
        {
            if (await roleManager.FindByNameAsync(name) == null)
                await roleManager.CreateAsync(new IdentityRole(name));
        }
    }
}
