import type PriorityEnum from '@/lib/domain/enums/priority'
import type StateEnum from '@/lib/domain/enums/state'
import type { StoryInterface } from '@/lib/domain/interfaces/storyInterface'
import type { TaskInterface } from '@/lib/domain/interfaces/taskInterface'
import type { UserInterface } from '@/lib/domain/interfaces/userInterface'
import Task from '@/lib/domain/models/task'

class TaskService {
  noOfTasks: number
  selectedProjId: number | null

  constructor() {
    const storedNoOfTasks = localStorage.getItem('noOfTasks')
    this.noOfTasks = storedNoOfTasks ? parseInt(storedNoOfTasks, 10) : 0

    const proj = localStorage.getItem('selectedProject')
    if (proj) {
      const projj = JSON.parse(proj)
      this.selectedProjId = projj.id
    } else this.selectedProjId = null
  }

  GetNoOfTasks(): number {
    return this.noOfTasks
  }

  ChangeState(selectedTask: TaskInterface, changedState: StateEnum) {
    if (changedState === 'IN PROGRESS') {
      selectedTask.startDate = new Date()
      selectedTask.endDate = null
    } else if (changedState === 'DONE') {
      selectedTask.endDate = new Date()
    } else if (changedState === 'TODO') {
      selectedTask.startDate = null
      selectedTask.endDate = null
    }
    selectedTask.state = changedState
    localStorage.setItem(
      `task${selectedTask.id}_story${selectedTask.story.id}_proj${this.selectedProjId}`,
      JSON.stringify(selectedTask),
    )
  }

  AssignUser(selectedTask: TaskInterface, assignedUser: UserInterface | null) {
    selectedTask.signedUser = assignedUser
    localStorage.setItem(
      `task${selectedTask.id}_story${selectedTask.story.id}_proj${this.selectedProjId}`,
      JSON.stringify(selectedTask),
    )
  }

  GetSpecifiedTasksList(storyId: number, state: string): TaskInterface[] {
    const result = []

    for (let i = 0; i < this.noOfTasks; i++) {
      const taskRaw = localStorage.getItem(`task${i}_story${storyId}_proj${this.selectedProjId}`)
      if (taskRaw) {
        const task = JSON.parse(taskRaw) as TaskInterface
        if (task && task.state === state) {
          result.push(task)
        }
      }
    }
    return result
  }
  Create(
    name: string,
    desc: string,
    story: StoryInterface,
    prio: PriorityEnum,
    estTime: Date,
    state: StateEnum,
    startDate: Date | null,
    endDate: Date | null,
    signedUser: UserInterface | null,
    user: UserInterface,
  ): void {
    const task = new Task(
      this.noOfTasks,
      name,
      desc,
      story,
      prio,
      estTime,
      state,
      new Date(),
      startDate,
      endDate,
      signedUser,
      user,
    )

    localStorage.setItem(
      `task${this.noOfTasks}_story${story.id}_proj${this.selectedProjId}`,
      JSON.stringify(task),
    )
    this.noOfTasks += 1
    localStorage.setItem('noOfTasks', this.noOfTasks.toString())
  }

  Details(id: number, storyId: number): TaskInterface | null {
    const taskRaw = localStorage.getItem(`task${id}_story${storyId}_proj${this.selectedProjId}`)

    if (taskRaw) {
      const task = JSON.parse(taskRaw) as TaskInterface
      return task
    } else {
      return null
    }
  }

  Edit(
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
    signedUser: UserInterface | null,
    user: UserInterface,
  ): void {
    const task = new Task(
      id,
      name,
      desc,
      story,
      prio,
      estTime,
      state,
      createdAt,
      startDate,
      endDate,
      signedUser,
      user,
    )

    localStorage.setItem(
      `task${id}_story${story.id}_proj${this.selectedProjId}`,
      JSON.stringify(task),
    )
  }

  Delete(id: number, storyId: number): void {
    localStorage.removeItem(`task${id}_story${storyId}_proj${this.selectedProjId}`)
  }
}

export default TaskService
