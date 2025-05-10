<template>
  <mainLayout>
    <button
      class="btn mb-4 create-project-btn"
      :class="themeStore.isDarkMode ? 'btn-dark' : 'btn-success'"
      @click="goToCreateProject"
    >
      Create PROJECT
    </button>

    <table
      :class="[
        'table table-striped table-bordered',
        themeStore.isDarkMode ? 'table-dark' : 'table-light',
      ]"
    >
      <thead :class="[themeStore.isDarkMode ? 'table-dark' : 'table-success']">
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
          <td>
            <button class="btn btn-info" @click="goToDetailsProject(project.id)">
              Show details
            </button>
          </td>
          <td>
            <button class="btn btn-warning" @click="goToEditProject(project.id)">
              Edit project
            </button>
          </td>
          <td>
            <button class="btn btn-danger" @click="goToDeleteProject(project.id)">
              Delete project
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </mainLayout>
</template>

<script setup lang="ts">
import type { ProjectInterface } from '@/lib/domain/interfaces/projectInterface'

import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import ProjectService from '@/lib/application/services/projectService'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import { useThemeStore } from '@/lib/application/stores/theme'

const router = useRouter()
const projectList = ref<ProjectInterface[]>([])
const projectService = new ProjectService()
const noOfProjs = ref<number>(0)
const themeStore = useThemeStore()

onMounted(() => {
  noOfProjs.value = projectService.GetNoOfProjs()
  fetchProjectList()
})

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
  projectList.value = projectService.GetProjectsList()
}
</script>

<style scoped>
.create-project-btn {
  padding: 10px 20px;
  font-weight: bold;
  font-size: 16px;
  border-radius: 5px;
}

.create-project-btn:hover {
  background-color: #28a745;
  color: white;
  transform: scale(1.05);
}

.create-project-btn:focus {
  outline: none;
  box-shadow: 0 0 10px rgba(33, 136, 56, 0.7);
}

.table {
  width: 100%;
  margin-top: 20px;
  border-radius: 8px;
  border-color: #dee2e6;
}

.table th {
  text-align: center;
}

.table-striped tbody tr:nth-of-type(odd) {
  background-color: #f8f9fa;
}

.table th,
.table td {
  padding: 12px 15px;
  text-align: center;
}

.table-bordered {
  border: 1px solid #dee2e6;
}

.table-success {
  background-color: #28a745;
  color: white;
}

.table-success th {
  background-color: #28a745;
  color: white;
}

table button {
  font-weight: bold;
  transition:
    background-color 0.3s ease,
    transform 0.2s ease;
}

table button:hover {
  transform: scale(1.05);
}
.table-dark {
  background-color: #2c2c2c;
  color: #ffffff;
  border-color: #dee2e6;
}

.btn-info {
  background-color: #007bff; /* Default color for 'Show details' button */
  border-color: #007bff;
  color: white;
}

.btn-info:hover {
  background-color: #0056b3;
  border-color: #0056b3;
}

.dark-mode .btn-info {
  background-color: #c8c9c7; /* Dark mode color for 'Show details' button */
  border-color: white;
  color: black;
}

.btn-warning {
  background-color: #ffcc00; /* Default color for 'Edit project' button */
  border-color: #ffcc00;
  color: black;
}

.btn-warning:hover {
  background-color: #e6b800;
  border-color: #e6b800;
}

.dark-mode .btn-warning {
  background-color: #c8c9c7; /* Dark mode color for 'Show details' button */
  border-color: white;
  color: black;
}

.btn-danger {
  background-color: #dc3545;
  border-color: #dc3545;
  color: white;
}

.btn-danger:hover {
  background-color: #c82333;
  border-color: #c82333;
}
</style>
