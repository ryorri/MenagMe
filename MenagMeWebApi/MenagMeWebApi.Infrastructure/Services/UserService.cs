using AutoMapper;
using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Domain.Entities;
using MenagMeWebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MenagMeWebApi.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly ServiceExtension _serviceExtension;

        public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, ServiceExtension service)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _serviceExtension = service;
        }
        public async Task<UserDataDTO?> CreateUser(UserCreateDTO user)
        {
            var userDto = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(userDto, user.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userDto, user.Role);
            }

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception($"{error.Code} : {error.Description}");
                }
            }

            return _mapper.Map<UserDataDTO>(userDto);

        }

        public async Task<UserDataDTO?> GetUser(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            var userDto =  _mapper.Map<UserDataDTO>(user);

            if (user != null)
            {
                await _serviceExtension.AssignRoles(user);
            }
            else
                throw new Exception("User not found");

            return userDto;

        }

        public async Task<UserDataDTO?> LogIn(string username, string password)
        {

            var login = await _signInManager.PasswordSignInAsync(username, password, false, false);
            
            if (!login.Succeeded)
                throw new Exception("Please, check username and password");
            else
            {
                var userExist = await _userManager.FindByNameAsync(username);
                var user = await _serviceExtension.AssignRoles(userExist!);
                return user;
            }
                

        }

        public async Task<List<UserDataDTO>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDto = new List<UserDataDTO>();

            foreach (var user in users)
            {
                userDto.Add(await _serviceExtension.AssignRoles(user));
            }
            return userDto;
        }
    }
}