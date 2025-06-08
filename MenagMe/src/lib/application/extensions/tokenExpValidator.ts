import { jwtDecode } from 'jwt-decode'

export function isTokenExpired(token: string): boolean {
  try {
    const decoded: { exp: number } = jwtDecode(token)
    const currentTime = Math.floor(Date.now() / 1000)
    if (currentTime >= decoded.exp) return true
    else return false
  } catch (error) {
    console.error('Error decoding token:', error)
    return false
  }
}
