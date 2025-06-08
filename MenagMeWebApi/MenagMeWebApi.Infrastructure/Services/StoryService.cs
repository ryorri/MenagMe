using AutoMapper;
using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.ProjectDTO;
using MenagMeWebApi.Application.Objects.StoriesDTO;
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
    public class StoryService : IStoryService
    {
        private readonly MenagMeMongoDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public StoryService(MenagMeMongoDbContext dbContext, IMapper mapper, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> ChangePriority(string storyId, string newPriority)
        {
            if (!Enum.TryParse<PriorityEnum>(newPriority, ignoreCase: true, out var parsedPriority))
            {
                throw new ArgumentException($"Invalid priority value: {newPriority}");
            }
            var story = await _dbContext.Stories
                                .Find(p => p.Id == storyId)
                                .FirstOrDefaultAsync();
            if (story == null)
            {
                throw new ArgumentException($"Story with ID {storyId} does not exist.");
            }
            else
            {
                story.Priority = parsedPriority;
                await _dbContext.Stories.ReplaceOneAsync(
                    p => p.Id == storyId, story);
                return true;
            }
                

        }

        public async Task<bool> ChangeState(string storyId, string newState)
        {
            if (!Enum.TryParse<StateEnum>(newState, ignoreCase: true, out var parsedState))
            {
                throw new ArgumentException($"Invalid priority value: {newState}");
            }
            var story = await _dbContext.Stories
                                .Find(p => p.Id == storyId)
                                .FirstOrDefaultAsync();
            if (story == null)
            {
                throw new ArgumentException($"Story with ID {storyId} does not exist.");
            }
            else
            {
                story.Status = parsedState;

                await _dbContext.Stories.ReplaceOneAsync(
                    p => p.Id == storyId, story);
                return true;
            }
        }

        public async Task<bool> CreateStory(StoryCreateDTO storyDTO)
        {
            if(storyDTO == null)
            {
                throw new ArgumentNullException(nameof(storyDTO), "Project cannot be null");
            }
            else
            {
                var user = await _userManager.FindByIdAsync(storyDTO.UserId);
                if (user == null)
                {
                    throw new ArgumentException("User does not exist", nameof(storyDTO.UserId));
                }

                var project = _mapper.Map<Story>(storyDTO);
                await _dbContext.Stories.InsertOneAsync(project);
                return true;
            }
        }

        public async Task<bool> DeleteStory(string storyId)
        {
            if (storyId == null)
            {
                throw new ArgumentNullException(nameof(storyId), "Project ID cannot be null");
            }
            else
            {
                
                var deleteResult = await _dbContext.Stories.DeleteOneAsync(p => p.Id == storyId);
                return true;
            }
        }

        public async Task<List<StoryDataDTO>> GetAllStories()
        {
            var stories = await _dbContext.Stories.Find(_ => true).ToListAsync();
            return _mapper.Map<List<StoryDataDTO>>(stories);
        }

        public async Task<List<StoryDataDTO>> GetStoriesByProject(string projectId)
        {
            var stories = await _dbContext.Stories.Find(p => p.ProjectId == projectId).ToListAsync();
            return _mapper.Map<List<StoryDataDTO>>(stories);
        }

        public async Task<StoryDataDTO?> GetStory(string storyId)
        {
            var story = await _dbContext.Stories
                                .Find(p => p.Id == storyId)
                                .FirstOrDefaultAsync();
            return _mapper.Map<StoryDataDTO>(story);
        }

        public async Task<bool> UpdateStory(string storyId, StoryCreateDTO newStory)
        {
            if(storyId == null)
            {
                throw new ArgumentNullException(nameof(storyId), "Project ID cannot be null");
            }
            else
            {
                var oldStory = await _dbContext.Stories
                                .Find(p => p.Id == storyId)
                                .FirstOrDefaultAsync();

                var newSto = _mapper.Map<Story>(newStory);

                newSto.Id = oldStory.Id;
                newSto.UserId = oldStory.UserId;


                await _dbContext.Stories.ReplaceOneAsync(
                    p => p.Id == storyId, newSto);

                return true;
            }
            
        }
    }
}
