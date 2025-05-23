﻿using AutoMapper;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region UserMapping
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserDataDTO, User>();
            CreateMap<User, UserDataDTO>();
            CreateMap<List<User>, List<UserDataDTO>>()
                    .ConvertUsing((src, dest, context) =>
                    {
                        var result = new List<UserDataDTO>();
                        foreach (var user in src)
                        {
                            result.Add(context.Mapper.Map<UserDataDTO>(user));
                        }
                        return result;
                    });
            #endregion
        }
    }
}
