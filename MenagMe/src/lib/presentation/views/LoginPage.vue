<template>
  <div :class="['login-bg', themeStore.isDarkMode ? 'dark-mode' : 'bg-light']">
    <div class="form-container">
      <h1>Sign in</h1>
      <form @submit.prevent="LogIn">
        <div class="form-group">
          <label for="name">Login</label>
          <input v-model="data.login" id="login" class="form-control" required />
        </div>
        <div class="form-group">
          <label for="desc">Password</label>
          <input type="password" v-model="data.psw" id="psw" class="form-control" required />
        </div>
        <button type="submit" class="btn btn-success w-100 mb-2">Login</button>
      </form>
      <button
        @click="themeStore.toggleTheme"
        class="btn btn-outline-dark w-100"
        :class="themeStore.isDarkMode ? 'btn-light text-dark' : 'btn-dark text-white'"
      >
        {{ themeStore.isDarkMode ? 'Light Mode' : 'Dark Mode' }}
      </button>
      <p id="err" v-if="data.error">Invalid username or password</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import UserService from '@/lib/application/services/userService'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useThemeStore } from '@/lib/application/stores/theme'

const userService = new UserService()
const router = useRouter()
const themeStore = useThemeStore()

const data = ref({
  login: '',
  psw: '',
  error: false,
})

const LogIn = async () => {
  const login = await userService.Login(data.value.login, data.value.psw)
  if (login) {
    router.push({ name: 'home' })
  } else data.value.error = true
}

onMounted(() => {
  data.value.login = ''
  data.value.psw = ''
  data.value.error = false
})
</script>

<style scoped>
.login-bg {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: #f8f9fa;
}
.dark-mode.login-bg {
  background: #181a1b;
}
.form-container {
  max-width: 400px;
  width: 100%;
  margin: 40px auto;
  padding: 32px 24px 24px 24px;
  background: #fff;
  border-radius: 12px;
  box-shadow: 0 2px 16px rgba(0, 0, 0, 0.08);
}
.dark-mode .form-container {
  background: #23272b;
  color: #f1f1f1;
  box-shadow: 0 2px 16px rgba(0, 0, 0, 0.25);
}
h1 {
  text-align: center;
  margin-bottom: 24px;
  color: #28a745;
}
.dark-mode h1 {
  color: #90ee90;
  /* Make it more visible in dark mode */
  text-shadow: 0 2px 8px #1d2124;
}
.form-group {
  margin-bottom: 18px;
}
label {
  display: block;
  margin-bottom: 6px;
  font-weight: 500;
}
input.form-control,
textarea.form-control {
  width: 100%;
  padding: 10px 12px;
  border: 1px solid #ced4da;
  border-radius: 6px;
  background: #f8f9fa;
  color: #212529;
  font-size: 16px;
  transition: border-color 0.2s;
}
input.form-control:focus,
textarea.form-control:focus {
  border-color: #28a745;
  outline: none;
}
.dark-mode input.form-control,
.dark-mode textarea.form-control {
  background: #2c2c2c;
  color: #f1f1f1;
  border: 1px solid #444950;
}
.btn-success {
  background-color: #28a745;
  border-color: #28a745;
  color: #fff;
}
.btn-success:hover {
  background-color: #218838;
  border-color: #218838;
}
.btn-dark {
  background-color: #343a40;
  border-color: #343a40;
  color: #fff;
}
.btn-dark:hover {
  background-color: #23272b;
  border-color: #23272b;
}
.btn-light {
  background-color: #f8f9fa;
  border-color: #f8f9fa;
  color: #23272b;
}
.btn-light:hover {
  background-color: #e2e6ea;
  border-color: #dae0e5;
}
.btn-outline-dark {
  border: 1.5px solid #343a40;
  color: #343a40;
  background: transparent;
}
.btn-outline-dark:hover {
  background: #343a40;
  color: #fff;
}
.btn-outline-light {
  border: 1.5px solid #f8f9fa;
  color: #f8f9fa;
  background: transparent;
}
.btn-outline-light:hover {
  background: #f8f9fa;
  color: #23272b;
}
p {
  color: #dc3545;
  text-align: center;
  margin-top: 16px;
}
.dark-mode p {
  color: #ff6b6b;
}
.btn-light.text-dark {
  background-color: #f8f9fa;
  border-color: #f8f9fa;
  color: #23272b;
}
.dark-mode .btn-light.text-dark {
  background-color: #ffffff;
  border-color: #c8c9c7;
  color: #23272b;
  font-weight: bold;
  box-shadow: 0 2px 8px #181a1b;
}
</style>
