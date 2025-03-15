import type PriorityEnum from '../enums/priority'
import type StateEnum from '../enums/state'
import type Project from './project'
import type User from './user'
import type { StoryInterface } from '../interfaces/storyInterface'

class Story implements StoryInterface {
  id: number
  name: string
  desc: string
  prio: PriorityEnum
  project: Project
  creationDate: Date
  state: StateEnum
  user: User

  constructor(
    id: number,
    name: string,
    desc: string,
    prio: PriorityEnum,
    project: Project,
    date: Date,
    state: StateEnum,
    user: User,
  ) {
    this.id = id
    this.name = name
    this.desc = desc
    this.prio = prio
    this.project = project
    this.creationDate = date
    this.state = state
    this.user = user
  }
}

export default Story
