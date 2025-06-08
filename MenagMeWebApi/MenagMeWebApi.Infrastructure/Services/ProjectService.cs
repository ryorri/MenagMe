using AutoMapper;
using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.ProjectDTO;
using MenagMeWebApi.Domain.Entities;
using MenagMeWebApi.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly MenagMeMongoDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProjectService(MenagMeMongoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> CreateProject(ProjectCreateDTO projectDTO)
        {
            if(projectDTO == null)
            {
                throw new ArgumentNullException(nameof(projectDTO), "Project cannot be null");
            }
            else
            {
                var project = _mapper.Map<Project>(projectDTO);
                await _dbContext.Projects.InsertOneAsync(project);
                return true;
            }
        }

        public async Task<bool> DeleteProject(string projectId)
        {
            if (projectId == null)
            {
                throw new ArgumentNullException(nameof(projectId), "Project ID cannot be null");
            }
            else
            {
                var deleteResult = await _dbContext.Projects.DeleteOneAsync(p => p.Id == projectId);
                return true;
            }
        }

        public async Task<List<ProjectDataDTO>> GetAllProjects()
        {
            var projects = await _dbContext.Projects.Find(_ => true).ToListAsync();
            return _mapper.Map<List<ProjectDataDTO>>(projects);
        }

        public async Task<ProjectDataDTO?> GetProject(string projectId)
        {
            var project = await _dbContext.Projects
                                .Find(p => p.Id == projectId)
                                .FirstOrDefaultAsync();
            return _mapper.Map<ProjectDataDTO>(project);
        }

        public async Task<bool> UpdateProject(string projectId, ProjectCreateDTO newProject)
        {
            if(projectId == null)
            {
                throw new ArgumentNullException(nameof(projectId), "Project ID cannot be null");
            }
            else
            {
                var oldProject = await _dbContext.Projects
                                .Find(p => p.Id == projectId)
                                .FirstOrDefaultAsync();

                var newProj = _mapper.Map<Project>(newProject);

                newProj.Id = oldProject.Id;


                await _dbContext.Projects.ReplaceOneAsync(
                    p => p.Id == projectId, newProj);

                return true;
            }
            
        }
    }
}
