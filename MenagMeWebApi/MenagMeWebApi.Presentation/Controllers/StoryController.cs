using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.ProjectDTO;
using MenagMeWebApi.Application.Objects.StoriesDTO;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenagMeWebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;
        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;

        }

        [Authorize]
        [HttpPost("create-story")]
        public async Task<ActionResult<bool>> CreateStory(StoryCreateDTO dto)
        {
            try
            {
                var result = await _storyService.CreateStory(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("detele-story")]
        public async Task<ActionResult<bool>> DeleteStory(string storyId)
        {
            try
            {
                var result = await _storyService.DeleteStory(storyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("get-stories-list")]
        public async Task<ActionResult<List<StoryDataDTO>>> GetStoriesList()
        {
            try
            {
                var result = await _storyService.GetAllStories();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("get-stories-list-by-project")]
        public async Task<ActionResult<List<StoryDataDTO>>> GetStoriesListByProject(string projectId)
        {
            try
            {
                var result = await _storyService.GetStoriesByProject(projectId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("get-story")]
        public async Task<ActionResult<StoryDataDTO?>> GetStory(string storyId)
        {
            try
            {
                var result = await _storyService.GetStory(storyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("update-story")]
        public async Task<ActionResult<bool>> UpdateStory(string storyId, StoryCreateDTO newStory)
        {
            try
            {
                var result = await _storyService.UpdateStory(storyId, newStory);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }
        [Authorize]
        [HttpPut("update-priority")]
        public async Task<ActionResult<bool>> UpdateStoryPriority(string storyId, string newPriority)
        {
            try
            {
                var result = await _storyService.ChangePriority(storyId, newPriority);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }

        [Authorize]
        [HttpPut("update-state")]
        public async Task<ActionResult<bool>> UpdateStoryState(string storyId, string newState)
        {
            try
            {
                var result = await _storyService.ChangeState(storyId, newState);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }
    }
}
