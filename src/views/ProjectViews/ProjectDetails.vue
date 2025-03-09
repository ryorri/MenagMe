<template>
  {{ project?.name }}
  {{ project?.desc }}
</template>

<script setup lang="ts">
import ProjectService from '@/lib/application/services/projectService'
import { useRoute } from 'vue-router'

const route = useRoute()

const projectId = Number(route.params.id)

const projectService = new ProjectService()

const project = projectService.Details(projectId)
</script>
