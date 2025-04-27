using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Interfaces.ServiceInterfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDataDTO user);
        string GenerateRefreshToken();
    }
}
