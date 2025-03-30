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

  GetNoOfProjs(): number {
    return this.noOfStories
  }

  GetStoriesList(projectId: number): StoryInterface[] {
    const result: StoryInterface[] = []

    for (let i = 0; i < this.noOfStories; i++) {
      const projRaw = localStorage.getItem(`story${i}_${projectId}`)
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

    localStorage.setItem(`story${this.noOfStories}_${project.id}`, JSON.stringify(story))

    this.noOfStories += 1

    localStorage.setItem('noOfStories', this.noOfStories.toString())
  }

  Details(id: number, project: ProjectInterface): StoryInterface | null {
    const projRaw = localStorage.getItem(`story${id}_${project.id}`)

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
    date: Date,
    state: StateEnum,
    user: User,
  ): void {
    const proj = new Story(id, name, desc, prio, project, date, state, user)

    localStorage.setItem(`story${id}_${project.id}`, JSON.stringify(proj))
  }

  Delete(id: number, project: ProjectInterface): void {
    localStorage.removeItem(`story${id}_${project.id}`)
  }
}

export default StoryService
