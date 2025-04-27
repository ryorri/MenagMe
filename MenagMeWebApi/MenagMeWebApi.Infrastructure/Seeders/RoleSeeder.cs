using MenagMeWebApi.Application.Interfaces.ISeederInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Infrastructure.Seeders
{
    public class RoleSeeder : IRoleSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public RoleSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task SeedRolesAsync()
        {
            string[] roles = { "Admin", "Developer", "DevOps" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    };
                    await _roleManager.CreateAsync(identityRole);
                }
            }
        }
    }

}
