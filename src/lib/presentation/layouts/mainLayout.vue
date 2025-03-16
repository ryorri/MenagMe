<template>
  <div class="d-flex flex-column vh-100">
    <!-- Header -->
    <header
      class="bg-success text-white p-3 d-flex justify-content-between align-items-center shadow"
    >
      <!--Login-->
      <div v-if="user" class="user-box d-flex align-items-center">
        <span>{{ user?.name }} {{ user?.surname }}</span>
        <button class="btn btn-outline-light btn-sm log-out-btn">Log out</button>
      </div>

      <!--Select project-->
      <div class="user-box d-flex align-items-center">
        Select project
        <select @change="selectProject">
          <option selected v-if="selectedProject">{{ selectedProject.name }}</option>
          <option :value="'null'"></option>
          <option v-for="project in projectList" :key="project.id" :value="project.id">
            {{ project.name }}
          </option>
        </select>
      </div>
      <h2 class="m-0 text-center flex-grow-1">MenagMe</h2>
    </header>

    <div class="d-flex flex-grow-1">
      <!-- Sidebar -->
      <nav class="bg-light border-end p-3 sidebar">
        <ul class="nav flex-column">
          <li class="nav-item">
            <button @click="goToProjects" class="nav-link text-success">📂 Projects</button>
          </li>
          <li class="nav-item">
            <button @click="goToStories" class="nav-link text-success">📂 Stories</button>
          </li>
        </ul>
      </nav>

      <!-- Main -->
      <main class="p-4 flex-grow-1 bg-light">
        <slot />
      </main>
    </div>

    <!-- Footer -->
    <footer class="bg-success text-white text-center p-2">
      <p>&copy; 2025 MenagMe</p>
    </footer>
  </div>
</template>

<style scoped>
.sidebar {
  width: 250px;
}

.nav-link {
  font-weight: bold;
}

.nav-link:hover {
  background-color: #28a745;
  color: white !important;
  border-radius: 5px;
}

.user-box {
  background-color: rgba(255, 255, 255, 0.2);
  border: 2px solid white;
  border-radius: 10px;
  padding: 5px 10px;
  font-weight: bold;
  box-shadow: 0 0 5px rgba(255, 255, 255, 0.3);
  width: 10vw;
  height: 8vh;
}
</style>

<script setup lang="ts">
import type { ProjectInterface } from '@/lib/domain/interfaces/projectInterface'

import ProjectService from '@/lib/application/services/projectService'
import UserService from '@/lib/application/services/userService'
import { ref, onMounted, onUnmounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const userService = new UserService()
const projectService = new ProjectService()
const user = userService.GetCurrentUser()
const projectList = ref<ProjectInterface[]>([])
const listInterval = ref()
const selectedProjectInterval = ref()
const selectedProject = ref()

onMounted(() => {
  fetchProjectList()
  fetchSelectedProject()
  listInterval.value = setInterval(fetchProjectList, 30000)
  selectedProjectInterval.value = setInterval(fetchSelectedProject, 30000)
})

onUnmounted(() => {
  if (listInterval.value) {
    clearInterval(listInterval.value)
  }
  if (selectedProjectInterval.value) {
    clearInterval(selectedProjectInterval.value)
  }
})

const fetchSelectedProject = () => {
  selectedProject.value = projectService.GetSelectProject()
}

const fetchProjectList = () => {
  projectList.value = projectService.GetProjectsList()
}

const selectProject = (event: Event) => {
  const target = event.target as HTMLSelectElement
  const selectedProjectId = ref()

  if (target.value != 'null') {
    selectedProjectId.value = target.value

    projectService.SetSelectProject(Number(selectedProjectId.value))
  } else {
    selectedProjectId.value = null
    projectService.SetSelectProject(selectedProjectId.value)
  }

  router.push({ name: 'home' })
}

const goToProjects = () => {
  router.push({ name: 'ProjectList' })
}

const goToStories = () => {
  if (selectedProject.value) {
    router.push({ name: 'StoryList', params: { projectId: selectedProject.value.id } })
  } else {
    router.push({ name: 'NoProject' })
  }
}
</script>
