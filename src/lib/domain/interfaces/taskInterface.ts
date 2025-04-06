import type PriorityEnum from '../enums/priority'
import type StateEnum from '../enums/state'
import type User from '../models/user'
import type { StoryInterface } from './storyInterface'

export interface TaskInterface {
  id: number
  name: string
  desc: string
  story: StoryInterface
  prio: PriorityEnum
  estimatedTime: Date
  state: StateEnum
  createdAt: Date
  startDate: Date | null
  endDate: Date | null
  user: User
  signedUser: User | null
}
