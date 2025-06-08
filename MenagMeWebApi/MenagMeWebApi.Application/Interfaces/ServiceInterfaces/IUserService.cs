using MenagMeWebApi.Application.Objects.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserDataDTO?> CreateUser(UserCreateDTO user);
        Task<UserDataDTO?> GetUser(string id);
        Task<UserDataDTO?> LogIn(string username, string password);
        Task<List<UserDataDTO>> GetAllUsers();
        Task<List<UserDataDTO>> GetUsersByRole(string role);
        Task<bool> CheckRefreshToken (string userId, string refreshToken);
        Task<bool> AssignNewRefreshToken(string userId, string refreshToken);
        Task<string> GetRefreshTokenFromDB(string userId);
    }
}
