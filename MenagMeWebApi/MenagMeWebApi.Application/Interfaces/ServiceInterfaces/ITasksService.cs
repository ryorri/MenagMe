using MenagMeWebApi.Application.Objects.StoriesDTO;
using MenagMeWebApi.Application.Objects.TasksDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Interfaces.ServiceInterfaces
{
    public interface ITasksService
    {
        Task<bool> CreateTask(TasksCreateDTO task);
        Task<bool> UpdateTask(string taskId, TasksCreateDTO newTask);
        Task<bool> DeleteTask(string taskId);
        Task<List<TasksDataDTO>> GetAllTasks();
        Task<List<TasksDataDTO>> GetTasksByStoryId(string storyId);
        Task<TasksDataDTO?> GetTask(string taskId);

        Task<bool> ChangeStateAndSetStartOrEnd(string taskId, string newState);
        Task<bool> ChangePriority(string taskId, string newPriority);

        Task<bool> AssignUser(string taskId, string userId);
    }
}
