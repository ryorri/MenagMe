<template>
  <div class="form-container">
    <h1>Create Story</h1>

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
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const data = ref({
  login: '',
  psw: '',
})

const LogIn = () => {
  router.push({ name: 'HomeView' })
}
</script>
