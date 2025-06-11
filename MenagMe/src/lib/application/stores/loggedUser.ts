// stores/auth.ts
import { defineStore } from 'pinia'
import { useRouter } from 'vue-router'

const router = useRouter()

export const userLoggedUserStore = defineStore('auth', {
  state: () => ({
    user: JSON.parse(localStorage.getItem('loggedUser') || 'null'),
  }),

  getters: {
    isAuthenticated: (state) => !!state.user,
  },

  actions: {
    login(userData: any) {
      this.user = userData
      localStorage.setItem('loggedUser', JSON.stringify(userData))
    },

    logout() {
      this.user = null
      localStorage.removeItem('loggedUser')
      router.push({ name: 'login' })
    },

    loadFromStorage() {
      const stored = localStorage.getItem('loggedUser')
      if (stored) {
        return true
      } else return false
    },
  },
})
