import type { UserDataDTO } from '@/backend/BaseApi'
import type PriorityEnum from '../enums/priority'
import type StateEnum from '../enums/state'
import type Project from '../models/project'
import type User from '../models/user'

export interface StoryInterface {
  id: number
  name: string
  desc: string
  prio: PriorityEnum
  project: Project
  createdAt: Date
  state: StateEnum
  user: UserDataDTO | undefined
}
