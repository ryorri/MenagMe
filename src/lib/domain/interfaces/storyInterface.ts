import type PriorityEnum from '../enums/priority'
import type StateEnum from '../enums/state'
import type Project from '../models/project'
import type User from '../models/user'

export interface Story {
  id: number
  name: string
  desc: string
  prio: PriorityEnum
  project: Project
  creationDate: Date
  state: StateEnum
  user: User
}
