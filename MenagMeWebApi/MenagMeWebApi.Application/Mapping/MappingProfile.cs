using AutoMapper;
using MenagMeWebApi.Application.Objects.ProjectDTO;
using MenagMeWebApi.Application.Objects.StoriesDTO;
using MenagMeWebApi.Application.Objects.TasksDTO;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Domain.Entities;

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

            #region ProjectMapping
            CreateMap<ProjectCreateDTO, Project>();
            CreateMap<ProjectDataDTO, Project>();
            CreateMap<Project, ProjectDataDTO>();
            CreateMap<List<Project>, List<ProjectDataDTO>>()
                    .ConvertUsing((src, dest, context) =>
                    {
                        var result = new List<ProjectDataDTO>();
                        foreach (var project in src)
                        {
                            result.Add(context.Mapper.Map<ProjectDataDTO>(project));
                        }
                        return result;
                    });
            #endregion

            #region StoriesMapping
            CreateMap<StoryCreateDTO, Story>();
            CreateMap<StoryDataDTO, Story>();
            CreateMap<Story, StoryDataDTO>();
            CreateMap<List<Story>, List<StoryDataDTO>>()
                    .ConvertUsing((src, dest, context) =>
                    {
                        var result = new List<StoryDataDTO>();
                        foreach (var project in src)
                        {
                            result.Add(context.Mapper.Map<StoryDataDTO>(project));
                        }
                        return result;
                    });
            #endregion

            #region TasksMapping
            CreateMap<TasksCreateDTO, Tasks>();
            CreateMap<TasksDataDTO, Tasks>();
            CreateMap<Tasks, TasksDataDTO>();
            CreateMap<List<Tasks>, List<TasksDataDTO>>()
                    .ConvertUsing((src, dest, context) =>
                    {
                        var result = new List<TasksDataDTO>();
                        foreach (var project in src)
                        {
                            result.Add(context.Mapper.Map<TasksDataDTO>(project));
                        }
                        return result;
                    });
            #endregion
        }
    }
}
