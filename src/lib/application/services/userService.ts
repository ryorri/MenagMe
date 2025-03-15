import User from '../../domain/models/user'

class UserService {
  constructor() {}

  GetCurrentUser(): string | null {
    const userRaw = localStorage.getItem('currentUser')

    if (userRaw) {
      const user = JSON.parse(userRaw)
      return `${user.name} ${user.surname}`
    } else return null
  }
  Create(id: number, name: string, sur: string) {
    const user = new User(id, name, sur)

    localStorage.setItem('currentUser', JSON.stringify(user))
  }
}
export default UserService
