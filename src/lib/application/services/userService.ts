import type { UserInterface } from '@/lib/domain/interfaces/userInterface'
import User from '../../domain/models/user'
import type RolesEnum from '@/lib/domain/enums/roles'

class UserService {
  constructor() {}

  GetCurrentUser(): UserInterface | null {
    const userRaw = localStorage.getItem('currentUser')

    if (userRaw) {
      const user = JSON.parse(userRaw)
      return user
    } else return null
  }
  Create(id: number, name: string, sur: string, role: RolesEnum) {
    const user = new User(id, name, sur, role)

    localStorage.setItem(name + sur, JSON.stringify(user))
  }
  Login(name: string, sur: string) {
    const user = localStorage.getItem(name + sur)

    if (user) {
      localStorage.setItem('currentUser', user)
    }
  }
}
export default UserService
