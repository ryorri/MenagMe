import { defineStore } from 'pinia'
import { ref, watch } from 'vue'

export const useThemeStore = defineStore('theme', () => {
  const isDarkMode = ref(false)

  const toggleTheme = () => {
    isDarkMode.value = !isDarkMode.value
    localStorage.setItem('isDarkMode', isDarkMode.value.toString())
  }

  const initializeTheme = () => {
    const storedTheme = localStorage.getItem('isDarkMode')
    isDarkMode.value = storedTheme === 'true'
  }

  // Watch for changes in localStorage and update isDarkMode dynamically
  watch(
    () => localStorage.getItem('isDarkMode'),
    (newValue) => {
      isDarkMode.value = newValue === 'true'
    },
  )

  return { isDarkMode, toggleTheme, initializeTheme }
})
