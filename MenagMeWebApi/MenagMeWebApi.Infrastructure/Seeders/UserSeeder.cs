using AutoMapper;
using MenagMeWebApi.Application.Interfaces.ISeederInterfaces;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Infrastructure.Seeders
{
    public class UserSeeder : IUserSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task SeedUsersAsync()
        {
            List<UserCreateDTO> users = new List<UserCreateDTO>
            {
                new UserCreateDTO
                {
                    Email = "j.adminowy@example.com",
                    UserName = "jadminowy",
                    Name = "Jan",
                    Surname = "Adminowy",
                    Password = "JanAdminowy1!",
                    Role = "Admin",
                },
                new UserCreateDTO
                {
                    Email = "m.developerowy@example.com",
                    UserName = "mdeveloperowy",
                    Name = "Michał",
                    Surname = "Developerowy",
                    Password = "MichalDeveloperowy1!",
                    Role = "Developer",
                },
                new UserCreateDTO
                {
                    Email = "j.devopsowy@example.com",
                    UserName = "jdevopsowy",
                    Name = "Józef",
                    Surname = "DevOpsowy",
                    Password = "JozefDevOpsowy1!",
                    Role = "DevOps",
                },
            };

            foreach (var user in users)
            {
                var usr = _mapper.Map<User>(user);
                var result = await _userManager.CreateAsync(usr, user.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usr, user.Role);
                }
                
            }


        }
    }
}
