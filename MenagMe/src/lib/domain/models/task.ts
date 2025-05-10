import type { UserDataDTO } from '@/backend/BaseApi'
import type PriorityEnum from '../enums/priority'
import type StateEnum from '../enums/state'
import type { StoryInterface } from '../interfaces/storyInterface'
import type { TaskInterface } from '../interfaces/taskInterface'
import type User from './user'

class Task implements TaskInterface {
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
  user: UserDataDTO | undefined
  signedUser: UserDataDTO | undefined

  constructor(
    id: number,
    name: string,
    desc: string,
    story: StoryInterface,
    prio: PriorityEnum,
    estTime: Date,
    state: StateEnum,
    createdAt: Date,
    startDate: Date | null,
    endDate: Date | null,
    signedUser: UserDataDTO | undefined,
    user: UserDataDTO | undefined,
  ) {
    this.id = id
    this.name = name
    this.desc = desc
    this.story = story
    this.prio = prio
    this.estimatedTime = estTime
    this.state = state
    this.createdAt = createdAt
    this.startDate = startDate
    this.endDate = endDate
    this.signedUser = signedUser
    this.user = user
  }
}

export default Task
