<template>
  <mainLayout>
    <button class="btn btn-success mb-4 create-project-btn" @click="goToCreateProject">
      Create PROJECT
    </button>

    <table class="table table-striped table-bordered">
      <thead class="table-success">
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

const router = useRouter()
const projectList = ref<ProjectInterface[]>([])
const projectService = new ProjectService()
const noOfProjs = ref<number>(0)

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
  transition:
    background-color 0.3s ease,
    transform 0.2s ease;
}

.create-project-btn:hover {
  background-color: #218838;
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

.btn-info {
  background-color: #17a2b8;
  border-color: #17a2b8;
  color: white;
}

.btn-info:hover {
  background-color: #138496;
  border-color: #138496;
}

.btn-warning {
  background-color: #ffc107;
  border-color: #ffc107;
  color: black;
}

.btn-warning:hover {
  background-color: #e0a800;
  border-color: #e0a800;
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
