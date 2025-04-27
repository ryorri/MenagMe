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
    }
}
