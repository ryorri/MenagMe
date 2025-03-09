<template>
  {{ project?.name }}
  {{ project?.desc }}

  <h3>
    Are you sure?
    <strong><button @click="deleteProject(projectId)">Delete!</button></strong>
  </h3>
</template>

<script setup lang="ts">
import ProjectService from '@/lib/application/services/projectService'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()

const projectId = Number(route.params.id)

const projectService = new ProjectService()

const project = projectService.Details(projectId)

const deleteProject = (id: number) => {
  projectService.Delete(id)
  router.push({ name: 'ProjectList' })
}
</script>
