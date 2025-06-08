using MenagMeWebApi.Application.Objects.ProjectDTO;
using MenagMeWebApi.Application.Objects.StoriesDTO;

namespace MenagMeWebApi.Application.Interfaces.ServiceInterfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProject(ProjectCreateDTO project);
        Task<bool> UpdateProject(string projectId, ProjectCreateDTO newProject);
        Task<bool> DeleteProject(string projectId);
        Task<List<ProjectDataDTO>> GetAllProjects();
        Task<ProjectDataDTO?> GetProject(string projectId);

    }
}
