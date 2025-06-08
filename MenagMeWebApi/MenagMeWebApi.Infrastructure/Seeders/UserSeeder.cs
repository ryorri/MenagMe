using AspNetCore.Identity.Mongo.Model;
using AutoMapper;
using MenagMeWebApi.Application.Interfaces.ISeederInterfaces;
using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Domain.Entities;
using MenagMeWebApi.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace MenagMeWebApi.Infrastructure.Seeders
{
    public class UserSeeder : IUserSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<MongoRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly MenagMeMongoDbContext _dbContext;
        public UserSeeder(UserManager<User> userManager, RoleManager<MongoRole> roleManager, IMapper mapper, ITokenService tokenService, MenagMeMongoDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _dbContext = dbContext;
        }

        public async Task SeedUsersAsync()
        {
            if (await _userManager.FindByEmailAsync("j.adminowy@example.com") is not User)
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
                        usr.RefreshToken = _tokenService.GenerateRefreshToken();
                        await _userManager.UpdateAsync(usr);
                        

                    }

                }
            }
        }          
    }
}
