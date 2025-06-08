using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.ProjectDTO;
using MenagMeWebApi.Application.Objects.StoriesDTO;
using MenagMeWebApi.Application.Objects.TasksDTO;
using MenagMeWebApi.Application.Objects.UserDTO;
using MenagMeWebApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenagMeWebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;
        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;

        }

        [Authorize]
        [HttpPost("create-task")]
        public async Task<ActionResult<bool>> CreateTask(TasksCreateDTO dto)
        {
            try
            {
                var result = await _tasksService.CreateTask(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("detele-task")]
        public async Task<ActionResult<bool>> DeleteTask(string taskId)
        {
            try
            {
                var result = await _tasksService.DeleteTask(taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("get-tasks-list")]
        public async Task<ActionResult<List<TasksDataDTO>>> GetTasksList()
        {
            try
            {
                var result = await _tasksService.GetAllTasks();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("get-tasks-list-by-story")]
        public async Task<ActionResult<List<TasksDataDTO>>> GetTaskListByStoryId(string storyId)
        {
            try
            {
                var result = await _tasksService.GetTasksByStoryId(storyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("get-task")]
        public async Task<ActionResult<TasksDataDTO?>> GetTask(string taskId)
        {
            try
            {
                var result = await _tasksService.GetTask(taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("update-task")]
        public async Task<ActionResult<bool>> UpdateTask(string taskId, TasksCreateDTO newTask)
        {
            try
            {
                var result = await _tasksService.UpdateTask(taskId, newTask);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }
        [Authorize]
        [HttpPut("update-task-priority")]
        public async Task<ActionResult<bool>> UpdateTaskPriority(string taskId, string newPriority)
        {
            try
            {
                var result = await _tasksService.ChangePriority(taskId, newPriority);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }

        [Authorize]
        [HttpPut("update-task-state-and-dates")]
        public async Task<ActionResult<bool>> UpdateTaskStateAndDates(string taskId, string newState)
        {
            try
            {
                var result = await _tasksService.ChangeStateAndSetStartOrEnd(taskId, newState);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }

        [Authorize]
        [HttpPut("assign-user-to-task")]
        public async Task<ActionResult<bool>> AssignUser(string taskId, string userId)
        {
            try
            {
                var result = await _tasksService.AssignUser(taskId, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }
    }
}
