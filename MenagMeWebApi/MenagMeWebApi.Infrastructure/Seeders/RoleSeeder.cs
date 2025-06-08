using AspNetCore.Identity.Mongo.Model;
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
        private readonly RoleManager<MongoRole> _roleManager;

        public RoleSeeder(RoleManager<MongoRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            var roles = new[] { "Admin", "Developer", "DevOps" };

            foreach (var role in roles)
            {
                var exists = await _roleManager.RoleExistsAsync(role);
                if (!exists)
                {
                    var result = await _roleManager.CreateAsync(new MongoRole(role));
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new Exception($"{role}: {errors}");
                    }
                }
            }
        }
    }
}