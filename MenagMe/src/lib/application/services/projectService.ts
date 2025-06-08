import { Backend } from '@/main'
import type { ProjectCreateDTO, ProjectDataDTO } from '@/backend/BaseApi'

class ProjectService {
  async CreateProject(project: ProjectCreateDTO): Promise<boolean | undefined> {
    try {
      await Backend.createProject(project)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async DeleteProject(id: string): Promise<boolean | undefined> {
    try {
      await Backend.deteleProject(id)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }
  async GetProjectList(): Promise<ProjectDataDTO[] | undefined> {
    try {
      const projectList = await Backend.getProjectList()
      return projectList
    } catch (ex) {
      console.log(ex)
    }
  }
  async GetProject(id: string): Promise<ProjectDataDTO | undefined> {
    try {
      const project = await Backend.getProject(id)
      return project
    } catch (ex) {
      console.log(ex)
    }
  }
  async UpdateProject(id: string, project: ProjectDataDTO): Promise<boolean | undefined> {
    try {
      await Backend.updateProject(id, project)
      return true
    } catch (ex) {
      console.log(ex)
    }
  }

  async SetSelectProject(id: string): Promise<void> {
    if (id == null || id === '') {
      localStorage.removeItem('selectedProject')
      return
    }
    const raw = await this.GetProject(id)

    if (id != null && raw != null) {
      localStorage.setItem('selectedProject', JSON.stringify(raw))
    }
  }

  GetSelectProject(): ProjectDataDTO | null {
    const raw = localStorage.getItem('selectedProject')
    if (raw) {
      return JSON.parse(raw) as ProjectDataDTO
    } else return null
  }
}

export default ProjectService
