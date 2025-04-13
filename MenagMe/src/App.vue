<script setup lang="ts">
import { RouterView } from 'vue-router'
import UserService from '@/lib/application/services/userService'
import { onMounted } from 'vue'
import RolesEnum from './lib/domain/enums/roles'

const userService = new UserService()

onMounted(() => {
  userService.Create(0, 'Jan', 'Adminowy', RolesEnum.admin)
  userService.Create(1, 'Józef', 'DevOpsowy', RolesEnum.devops)
  userService.Create(2, 'Michał', 'Developerowy', RolesEnum.dev)

  userService.Login('Jan', 'Adminowy')
})
</script>

<template>
  <RouterView />
</template>
