<template>
  <div class="form-container">
    <h1>Sing in</h1>

    <form @submit.prevent="LogIn">
      <div class="form-group">
        <label for="name">Login</label>
        <input v-model="data.login" id="login" class="form-control" required />
      </div>

      <div class="form-group">
        <label for="desc">Password</label>
        <textarea v-model="data.psw" id="psw" class="form-control" required></textarea>
      </div>
      <button type="submit">Login</button>
    </form>
    <p v-if="data.error">Invalid username or password</p>
  </div>
</template>

<script setup lang="ts">
import UserService from '@/lib/application/services/userService'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'

const userService = new UserService()

const router = useRouter()

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
  // Reset login state on mount
  data.value.login = ''
  data.value.psw = ''
  data.value.error = false
})
</script>
