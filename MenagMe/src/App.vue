<script setup lang="ts">
import { RouterView, useRouter } from 'vue-router'

import { onMounted } from 'vue'
import { userLoggedUserStore } from './lib/application/stores/loggedUser'

const loggedUserStore = userLoggedUserStore()

const router = useRouter()

onMounted(() => {
  let logged = loggedUserStore.loadFromStorage()

  if (logged) {
    return true
  } else {
    router.push({ name: 'login' })
    return false
  }
})
</script>

<template>
  <RouterView />
</template>
