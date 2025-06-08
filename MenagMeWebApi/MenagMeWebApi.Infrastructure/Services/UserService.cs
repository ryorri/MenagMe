using AutoMapper;
using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Domain.Entities;
using MenagMeWebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver.Linq;

namespace MenagMeWebApi.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly ServiceExtension _serviceExtension;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, ServiceExtension service, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _serviceExtension = service;
            _tokenService = tokenService;
        }
        public async Task<UserDataDTO?> CreateUser(UserCreateDTO user)
        {
            var userDto = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(userDto, user.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userDto, user.Role);
                userDto.RefreshToken = _tokenService.GenerateRefreshToken();
                await _userManager.UpdateAsync(userDto);
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

            var userRole = await _serviceExtension.AssignRoles(user!);


            return userRole;

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
        public async Task<List<UserDataDTO>> GetUsersByRole(string role)
        {
            var users = await _userManager.Users.ToListAsync();

            var usersDto = new List<UserDataDTO>();

            foreach (var user in users)
            {
                usersDto.Add(await _serviceExtension.AssignRoles(user));
            }
            var usersDtoByRole = new List<UserDataDTO>();

            foreach (var user in usersDtoByRole)
            {
                if (user.Role == role)
                {
                    usersDtoByRole.Add(user);
                }

            }
            return usersDtoByRole;
        }

        public async Task<bool> CheckRefreshToken(string userId, string refreshToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.RefreshToken == refreshToken)
                return true;

            return false;
        }

        public async Task<bool> AssignNewRefreshToken(string userId, string refreshToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                return true;
            }
                
            else
                return false;
        }

        public async Task<string> GetRefreshTokenFromDB(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {

                return user.RefreshToken;
            }
            else
                return string.Empty;
        }
    }
}