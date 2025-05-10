import type { UserDataDTO } from '@/backend/BaseApi'
import type RolesEnum from '../enums/roles'
import type { UserInterface } from '../interfaces/userInterface'

class User implements UserDataDTO {
  id: string
  name: string
  surname: string
  role: RolesEnum

  constructor(id: string, name: string, sur: string, role: RolesEnum) {
    this.id = id
    this.name = name
    this.surname = sur
    this.role = role
  }
}

export default User
