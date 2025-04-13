import type { StoryInterface } from '@/lib/domain/interfaces/storyInterface'
import PriorityEnum from '@/lib/domain/enums/priority'
import StateEnum from '@/lib/domain/enums/state'
import type { ProjectInterface } from '@/lib/domain/interfaces/projectInterface'
import Story from '@/lib/domain/models/story'
import User from '@/lib/domain/models/user'

class StoryService {
  noOfStories: number

  constructor() {
    const storedNoOfStories = localStorage.getItem('noOfStories')
    this.noOfStories = storedNoOfStories ? parseInt(storedNoOfStories, 10) : 0
  }

  GetNoOfStories(): number {
    return this.noOfStories
  }

  GetStory(id: number, projId: number): StoryInterface | null {
    const storyRaw = localStorage.getItem(`story${id}_proj${projId}`)
    if (storyRaw) {
      return JSON.parse(storyRaw)
    } else return null
  }

  GetStoriesList(projectId: number): StoryInterface[] {
    const result: StoryInterface[] = []

    for (let i = 0; i < this.noOfStories; i++) {
      const projRaw = localStorage.getItem(`story${i}_proj${projectId}`)
      if (projRaw) {
        const proj = JSON.parse(projRaw) as StoryInterface

        result.push(proj)
      }
    }
    return result
  }

  Create(
    name: string,
    desc: string,
    prio: PriorityEnum,
    project: ProjectInterface,
    state: StateEnum,
    user: User,
  ): void {
    const story = new Story(this.noOfStories, name, desc, prio, project, new Date(), state, user)

    localStorage.setItem(`story${this.noOfStories}_proj${project.id}`, JSON.stringify(story))

    this.noOfStories += 1

    localStorage.setItem('noOfStories', this.noOfStories.toString())
  }

  Details(id: number, projectId: number): StoryInterface | null {
    const projRaw = localStorage.getItem(`story${id}_proj${projectId}`)

    if (projRaw) {
      const proj = JSON.parse(projRaw) as StoryInterface
      return proj
    } else {
      return null
    }
  }

  Edit(
    id: number,
    name: string,
    desc: string,
    prio: PriorityEnum,
    project: ProjectInterface,
    createdAt: Date,
    state: StateEnum,
    user: User,
  ): void {
    const story = new Story(id, name, desc, prio, project, createdAt, state, user)

    localStorage.setItem(`story${id}_proj${project.id}`, JSON.stringify(story))
  }

  Delete(id: number, projectId: number): void {
    localStorage.removeItem(`story${id}_proj${projectId}`)
  }
}

export default StoryService
