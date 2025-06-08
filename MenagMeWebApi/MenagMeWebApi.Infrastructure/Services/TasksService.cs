using AutoMapper;
using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.StoriesDTO;
using MenagMeWebApi.Application.Objects.TasksDTO;
using MenagMeWebApi.Domain.Entities;
using MenagMeWebApi.Domain.Enums;
using MenagMeWebApi.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Infrastructure.Services
{
    public class TasksService : ITasksService
    {
        private readonly MenagMeMongoDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public TasksService(MenagMeMongoDbContext dbContext, IMapper mapper, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<bool> ChangePriority(string taskId, string newPriority)
        {
            if (!Enum.TryParse<PriorityEnum>(newPriority, ignoreCase: true, out var parsedPriority))
            {
                throw new ArgumentException($"Invalid priority value: {newPriority}");
            }
            var story = await _dbContext.Tasks
                                .Find(p => p.Id == taskId)
                                .FirstOrDefaultAsync();
            if (story == null)
            {
                throw new ArgumentException($"Story with ID {taskId} does not exist.");
            }
            else
            {
                story.Priority = parsedPriority;
                await _dbContext.Tasks.ReplaceOneAsync(
                    p => p.Id == taskId, story);
                return true;
            }
        }

        public async Task<bool> ChangeStateAndSetStartOrEnd(string taskId, string newState)
        {
            if (!Enum.TryParse<StateEnum>(newState, ignoreCase: true, out var parsedState))
            {
                throw new ArgumentException($"Invalid priority value: {newState}");
            }
            var task = await _dbContext.Tasks
                                .Find(p => p.Id == taskId)
                                .FirstOrDefaultAsync();
            if (task == null)
            {
                throw new ArgumentException($"Story with ID {taskId} does not exist.");
            }
            else
            {
                task.Status = parsedState;

                if (parsedState == StateEnum.InProgress)
                {
                    task.StartDate = DateTime.UtcNow;
                }
                else if (parsedState == StateEnum.Done)
                {
                    task.EndDate = DateTime.UtcNow;
                }
                else
                {
                    task.StartDate = null;
                    task.EndDate = null;
                }

                await _dbContext.Tasks.ReplaceOneAsync(
                    p => p.Id == taskId, task);
                return true;
            }
        }

        public async Task<bool> CreateTask(TasksCreateDTO taskDto)
        {
            if (taskDto == null)
            {
                throw new ArgumentNullException(nameof(taskDto), "Task cannot be null");
            }
            else
            {
                var user = await _userManager.FindByIdAsync(taskDto.UserId);
                if (user == null)
                {
                    throw new ArgumentException("User does not exist", nameof(taskDto.UserId));
                }

                var project = _mapper.Map<Tasks>(taskDto);
                await _dbContext.Tasks.InsertOneAsync(project);
                return true;
            }
        }

        public async Task<bool> DeleteTask(string taskId)
        {
            if (taskId == null)
            {
                throw new ArgumentNullException(nameof(taskId), "Project ID cannot be null");
            }
            else
            {

                var deleteResult = await _dbContext.Tasks.DeleteOneAsync((p => p.Id == taskId));
                return true;
            }
        }

        public async Task<List<TasksDataDTO>> GetAllTasks()
        {
            var tasks = await _dbContext.Tasks.Find(_ => true).ToListAsync();
            return _mapper.Map<List<TasksDataDTO>>(tasks);
        }

        public async Task<TasksDataDTO?> GetTask(string taskId)
        {
            var story = await _dbContext.Tasks
                               .Find(p => p.Id == taskId)
                               .FirstOrDefaultAsync();
            return _mapper.Map<TasksDataDTO>(story);
        }

        public async Task<bool> UpdateTask(string taskId, TasksCreateDTO newTask)
        {
            if (taskId == null)
            {
                throw new ArgumentNullException(nameof(taskId), "Project ID cannot be null");
            }
            else
            {
                var oldTask = await _dbContext.Tasks
                                .Find(p => p.Id == taskId)
                                .FirstOrDefaultAsync();

                var newTsk = _mapper.Map<Tasks>(newTask);

                newTsk.Id = oldTask.Id;
                newTsk.UserId = oldTask.UserId;


                await _dbContext.Tasks.ReplaceOneAsync(
                    p => p.Id == taskId, newTsk);

                return true;
            }
        }

        public async Task<bool> AssignUser(string taskId, string assignedUserId)
        {
            if (taskId == null)
            {
                throw new ArgumentNullException(nameof(taskId), "Project ID cannot be null");
            }
            else
            {
                var task = await _dbContext.Tasks
                                .Find(p => p.Id == taskId)
                                .FirstOrDefaultAsync();

                var user = await _userManager.FindByIdAsync(assignedUserId);
                if (user == null)
                {
                    throw new ArgumentException("User does not exist", nameof(task.UserId));
                }

                task.AssignedUserId = assignedUserId;

                await _dbContext.Tasks.ReplaceOneAsync(
                    p => p.Id == taskId, task);

                return true;
            }
        }
        public async Task<List<TasksDataDTO>> GetTasksByStoryId(string storyId)
        {
            var stories = await _dbContext.Tasks.Find(p => p.StoryId == storyId).ToListAsync();
            return _mapper.Map<List<TasksDataDTO>>(stories);
        }
    }
}
