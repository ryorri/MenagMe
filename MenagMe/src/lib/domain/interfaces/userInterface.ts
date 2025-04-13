import type RolesEnum from '../enums/roles'

export interface UserInterface {
  id: number
  name: string
  surname: string
  role: RolesEnum
}
