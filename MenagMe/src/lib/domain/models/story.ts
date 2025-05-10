import type PriorityEnum from '../enums/priority'
import type StateEnum from '../enums/state'
import type Project from './project'
import type User from './user'
import type { StoryInterface } from '../interfaces/storyInterface'
import type { UserDataDTO } from '@/backend/BaseApi'

class Story implements StoryInterface {
  id: number
  name: string
  desc: string
  prio: PriorityEnum
  project: Project
  createdAt: Date
  state: StateEnum
  user: UserDataDTO | undefined

  constructor(
    id: number,
    name: string,
    desc: string,
    prio: PriorityEnum,
    project: Project,
    createdAt: Date,
    state: StateEnum,
    user: UserDataDTO | undefined,
  ) {
    this.id = id
    this.name = name
    this.desc = desc
    this.prio = prio
    this.project = project
    this.createdAt = createdAt
    this.state = state
    this.user = user
  }
}

export default Story
