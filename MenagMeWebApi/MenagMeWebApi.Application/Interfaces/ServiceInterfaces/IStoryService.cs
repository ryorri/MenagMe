using MenagMeWebApi.Application.Objects.ProjectDTO;
using MenagMeWebApi.Application.Objects.StoriesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Interfaces.ServiceInterfaces
{
    public interface IStoryService
    {

        Task<bool> CreateStory(StoryCreateDTO story);
        Task<bool> UpdateStory(string storytId, StoryCreateDTO newStory);
        Task<bool> DeleteStory(string storytId);
        Task<List<StoryDataDTO>> GetAllStories();
        Task<List<StoryDataDTO>> GetStoriesByProject(string projectId);
        Task<StoryDataDTO?> GetStory(string storytId);

        Task<bool> ChangeState(string storyId, string newState);
        Task<bool> ChangePriority(string storyId, string newPriority);
    }
}
