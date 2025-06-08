import { Backend } from '@/main'
import type { TasksCreateDTO, TasksDataDTO } from '@/backend/BaseApi'

class TaskService {
  async CreateTask(task: TasksCreateDTO): Promise<boolean | undefined> {
    try {
      await Backend.createTask(task)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async DeleteTask(id: string): Promise<boolean | undefined> {
    try {
      await Backend.deteleTask(id)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async GetTaskList(): Promise<TasksDataDTO[] | undefined> {
    try {
      const taskList = await Backend.getTasksList()
      return taskList
    } catch (ex) {
      console.log(ex)
    }
  }

  async GetTask(id: string): Promise<TasksDataDTO | undefined> {
    try {
      const task = await Backend.getTask(id)
      return task
    } catch (ex) {
      console.log(ex)
    }
  }

  async UpdateTask(id: string, task: TasksDataDTO): Promise<boolean | undefined> {
    try {
      await Backend.updateTask(id, task)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async UpdateTaskStatus(id: string, status: string): Promise<boolean | undefined> {
    try {
      await Backend.updateTaskStateAndDates(id, status)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async UpdateTaskPriority(id: string, priority: string): Promise<boolean | undefined> {
    try {
      await Backend.updateTaskPriority(id, priority)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }
  async GetSpecificTaskListByStory(storyId: string): Promise<TasksDataDTO[] | undefined> {
    try {
      return await Backend.getTasksListByStory(storyId)
    } catch (ex) {
      console.log(ex)
    }
  }

  async AssignUser(storyId: string, userId: string): Promise<boolean | undefined> {
    try {
      await Backend.assignUserToTask(storyId, userId)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }
}

export default TaskService
export { TaskService }
