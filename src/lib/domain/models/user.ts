import type { UserInterface } from '../interfaces/userInterface'

class User implements UserInterface {
  id: number
  name: string
  surname: string

  constructor(id: number, name: string, sur: string) {
    this.id = id
    this.name = name
    this.surname = sur
  }
}

export default User
