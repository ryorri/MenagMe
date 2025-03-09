<template>
  <button @click="goToCreateProject">Create PROJECT</button>

  <table>
    <thead>
      <tr>
        <th>Name</th>
        <th>Description</th>
        <th>Details</th>
        <th>Edit</th>
        <th>Delete</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="project in projectList" :key="project.id">
        <td>{{ project.name }}</td>
        <td>{{ project.desc }}</td>
        <td><button @click="goToDetailsProject(project.id)">Show details</button></td>
        <td><button @click="goToEditProject(project.id)">Edit project</button></td>
        <td><button @click="goToDeleteProject(project.id)">Delete project</button></td>
      </tr>
    </tbody>
  </table>
</template>

<script setup lang="ts">
import { onMounted, onUnmounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import ProjectService from '@/lib/application/services/projectService'

interface ProjectInterface {
  id: number
  name: string
  desc: string
}
const router = useRouter()
const projectList = ref(<ProjectInterface[]>[])
const projectService = new ProjectService()
const noOfProjs = ref()

onMounted(() => {
  noOfProjs.value = projectService.GetNoOfProjs()
  fetchProjectList()
})

onUnmounted(() => {})

const goToCreateProject = () => {
  router.push({ name: 'ProjectCreate' })
}
const goToDetailsProject = (projId: number) => {
  router.push({ name: 'ProjectDetals', params: { id: projId } })
}
const goToDeleteProject = (projId: number) => {
  router.push({ name: 'ProjectDelete', params: { id: projId } })
}
const goToEditProject = (projId: number) => {
  router.push({ name: 'ProjectEdit', params: { id: projId } })
}

const fetchProjectList = () => {
  for (let i = 0; i < noOfProjs.value; i++) {
    const projData = localStorage.getItem('proj' + i)
    if (projData) {
      const project = JSON.parse(projData) as ProjectInterface
      projectList.value.push(project)
    }
  }
}
</script>
