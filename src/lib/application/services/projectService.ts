import type { ProjectInterface } from '@/lib/domain/interfaces/projectInterface'
import Project from '@/lib/domain/models/project'

class ProjectService {
  noOfProjs: number

  constructor() {
    const storedNoOfProjs = localStorage.getItem('noOfProjs')
    this.noOfProjs = storedNoOfProjs ? parseInt(storedNoOfProjs, 10) : 0
  }

  GetNoOfProjs(): number {
    return this.noOfProjs
  }

  Create(name: string, desc: string): void {
    const proj = new Project(this.noOfProjs, name, desc)

    localStorage.setItem('proj' + this.noOfProjs, JSON.stringify(proj))

    this.noOfProjs += 1

    localStorage.setItem('noOfProjs', this.noOfProjs.toString())
  }

  Details(id: number): ProjectInterface | null {
    const projRaw = localStorage.getItem('proj' + id)

    if (projRaw) {
      const proj = JSON.parse(projRaw) as ProjectInterface
      return proj
    } else {
      return null
    }
  }

  Edit(id: number, name: string, desc: string): void {
    const proj = new Project(id, name, desc)

    localStorage.setItem('proj' + id, JSON.stringify(proj))
  }

  Delete(id: number): void {
    localStorage.removeItem('proj' + id)
  }
}

export default ProjectService
