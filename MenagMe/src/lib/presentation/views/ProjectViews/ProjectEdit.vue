<template>
  <mainLayout>
    <div v-if="loading" class="project-details">
      <form @submit.prevent="editProject" class="form-container">
        <div class="form-group">
          <label for="name">Name:</label>
          <input id="name" v-model="editedProject!.name" type="text" required class="form-input" />
        </div>

        <div class="form-group">
          <label for="desc">Description:</label>
          <textarea
            id="desc"
            v-model="editedProject!.description"
            required
            class="form-input"
          ></textarea>
        </div>

        <button type="submit" class="submit-btn">Edit project</button>
      </form>
    </div>
    <div v-else>Loading...</div>
  </mainLayout>
</template>

<script setup lang="ts">
import ProjectService from '@/lib/application/services/projectService'
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import mainLayout from '../../layouts/mainLayout.vue'
import type { ProjectDataDTO } from '@/backend/BaseApi'

const route = useRoute()
const router = useRouter()

const projectId = String(route.params.id)

const projectService = new ProjectService()

const editedProject = ref<ProjectDataDTO>()
const loading = ref(false)

onMounted(() => {
  fetchProjectDetails()
})

const editProject = () => {
  if (editedProject.value) {
    projectService.UpdateProject(projectId, editedProject.value)
    router.push({ name: 'ProjectList' })
  }
}

const fetchProjectDetails = async () => {
  const projectDetails = await projectService.GetProject(projectId)
  if (projectDetails) {
    editedProject.value = {
      id: projectDetails.id,
      name: projectDetails.name,
      description: projectDetails.description,
    }
    loading.value = true
  }
}
</script>

<style scoped>
.project-details {
  max-width: 900px;
  margin: 30px auto;
  padding: 20px;
  background-color: #fafafa;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.project-name {
  font-size: 2rem;
  font-weight: 700;
  color: #2c3e50;
  margin-bottom: 15px;
}

.project-description {
  font-size: 1.1rem;
  color: #7f8c8d;
  margin-bottom: 20px;
  line-height: 1.6;
}

.form-container {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

label {
  font-size: 1rem;
  font-weight: 500;
  color: #2c3e50;
}

.form-input {
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 1rem;
  color: #2c3e50;
  transition: border-color 0.3s ease;
}

.form-input:focus {
  border-color: #28a745;
  outline: none;
}

.submit-btn {
  padding: 10px 20px;
  background-color: #28a745;
  color: white;
  font-size: 1.1rem;
  font-weight: bold;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.submit-btn:hover {
  background-color: #218838;
}

.submit-btn:focus {
  outline: none;
}
</style>
