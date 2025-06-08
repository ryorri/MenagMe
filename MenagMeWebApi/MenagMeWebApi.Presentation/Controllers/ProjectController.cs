using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.ProjectDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenagMeWebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;

        }

        [Authorize]
        [HttpPost("create-project")]
        public async Task<ActionResult<bool>> CreateProject(ProjectCreateDTO dto)
        {
            try
            {
                var result = await _projectService.CreateProject(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("detele-project")]
        public async Task<ActionResult<bool>> DeleteProject(string projectId)
        {
            try
            {
                var result = await _projectService.DeleteProject(projectId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("get-project-list")]
        public async Task<ActionResult<List<ProjectDataDTO>>> GetProjectList()
        {
            try
            {
                var result = await _projectService.GetAllProjects();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("get-project")]
        public async Task<ActionResult<ProjectDataDTO?>> GetProject(string projectId)
        {
            try
            {
                var result = await _projectService.GetProject(projectId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("update-project")]
        public async Task<ActionResult<bool>> UpdateProject(string projectId, ProjectCreateDTO newProject)
        {
            try
            {
                var result = await _projectService.UpdateProject(projectId, newProject);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }
    }
}
