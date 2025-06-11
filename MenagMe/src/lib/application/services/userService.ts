import { Backend } from '@/main'
import type { UserDataDTO, UserCreateDTO } from '@/backend/BaseApi'

class UserService {
  constructor() {}

  async GetCurrentUser(): Promise<UserDataDTO | undefined> {
    try {
      const userDataRaw = localStorage.getItem('loggedUser')

      if (userDataRaw) {
        const userDataParsed = JSON.parse(userDataRaw)
        return userDataParsed.result as UserDataDTO
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

  async Create(userCreateDTO: UserCreateDTO): Promise<UserCreateDTO | undefined> {
    try {
      const user = await Backend.register(userCreateDTO)
      return user
    } catch (ex) {
      console.log(ex)
    }
  }

  async Login(name: string, psw: string): Promise<UserDataDTO | undefined> {
    try {
      const user = await Backend.logIn(name, psw)
      if (user) {
        localStorage.setItem('loggedUser', JSON.stringify(user))
        await Backend.restoreTokens()
        return user
      }
    } catch (ex) {
      console.log(ex)
    }
  }

  Logout(): boolean {
    try {
      localStorage.removeItem('loggedUser')
      return true
    } catch (ex) {
      console.log(ex)
      return false
    }
  }

  async GetUser(id: string): Promise<UserDataDTO | undefined> {
    try {
      const user = await Backend.getUser(id)
      if (user) {
        return user
      }
    } catch (ex) {
      console.log(ex)
    }
  }

  async GetUserListByRole(role: string): Promise<UserDataDTO[] | undefined> {
    try {
      const userList = await Backend.getUserListByRole(role)
      return userList
    } catch (ex) {
      console.log(ex)
    }
  }
}
export default UserService
