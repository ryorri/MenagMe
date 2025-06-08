using AutoMapper;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Infrastructure.Extensions
{
    public class ServiceExtension
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public ServiceExtension(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserDataDTO> AssignRoles(User user)
        {
            var userDto = _mapper.Map<UserDataDTO>(user);

            var userId = user.Id.ToString();
            var exists = await _userManager.FindByIdAsync(userId);
            if (exists != null)
            {
                var roles = await _userManager.GetRolesAsync(exists);
                var rolesList = roles.ToList();
                userDto.Role = roles[0];
            }

            return userDto;
        }
    }
}
