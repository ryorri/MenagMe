import type { UserInterface } from '@/lib/domain/interfaces/userInterface'
import User from '../../domain/models/user'
import type RolesEnum from '@/lib/domain/enums/roles'
import { Backend } from '@/main'
import type { UserDataDTO } from '@/backend/BaseApi'
import { JsxEmit } from 'typescript'

class UserService {
  constructor() {}

  async GetCurrentUser(): Promise<UserDataDTO | undefined> {
    try {
      const userDataRaw = localStorage.getItem('loggedUser')

      if (userDataRaw) {
        const userDataParsed = JSON.parse(userDataRaw)
        const user = await Backend.getUser(userDataParsed.result.id)
        return user
      }
    } catch (ex) {
      console.log(ex)
    }
  }

  async GetUserList(): Promise<UserDataDTO[] | undefined> {
    try {
      const userList = await Backend.getUserList()
      return userList
    } catch (ex) {
      console.log(ex)
    }
  }
  Create(id: string, name: string, sur: string, role: RolesEnum) {
    const user = new User(id, name, sur, role)

    localStorage.setItem('user' + name + sur, JSON.stringify(user))
  }
  async Login(name: string, psw: string) {
    try {
      const user = await Backend.logIn(name, psw)
      if (user) {
        localStorage.setItem('loggedUser', JSON.stringify(user))
        return true
      }
    } catch {
      return false
    }
  }
}
export default UserService
