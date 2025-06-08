import { Backend } from '@/main'
import type { StoryCreateDTO, StoryDataDTO } from '@/backend/BaseApi'

class StoryService {
  async CreateStory(story: StoryCreateDTO): Promise<boolean | undefined> {
    try {
      await Backend.createStory(story)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async DeleteStory(id: string): Promise<boolean | undefined> {
    try {
      await Backend.deteleStory(id)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async GetStoryList(): Promise<StoryDataDTO[] | undefined> {
    try {
      const storyList = await Backend.getStoriesList()
      return storyList
    } catch (ex) {
      console.log(ex)
    }
  }

  async GetStoryListByProject(projectId: string): Promise<StoryDataDTO[] | undefined> {
    try {
      const storyList = await Backend.getStoriesListByProject(projectId)
      return storyList
    } catch (ex) {
      console.log(ex)
    }
  }

  async GetStory(id: string): Promise<StoryDataDTO | undefined> {
    try {
      const story = await Backend.getStory(id)
      return story
    } catch (ex) {
      console.log(ex)
    }
  }

  async UpdateStory(id: string, story: StoryDataDTO): Promise<boolean | undefined> {
    try {
      await Backend.updateStory(id, story)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async UpdateStoryStatus(id: string, status: string): Promise<boolean | undefined> {
    try {
      await Backend.updateState(id, status)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async UpdateStoryPriority(id: string, priority: string): Promise<boolean | undefined> {
    try {
      await Backend.updatePriority(id, priority)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }
}

export default StoryService
