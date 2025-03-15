<script setup lang="ts">
import { RouterView } from 'vue-router'
import UserService from '@/lib/application/services/userService'
import { onMounted } from 'vue'

const userService = new UserService()

onMounted(() => {
  userService.Create(0, 'Jan', 'Mockupowy')
})
</script>

<template>
  <RouterView />
</template>
