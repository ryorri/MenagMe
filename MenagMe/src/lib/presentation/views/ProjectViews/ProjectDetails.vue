<template>
  <mainLayout>
    <div class="project-detail-container">
      <h2 class="project-title">{{ project?.name }}</h2>
      <p class="project-description">{{ project?.description }}</p>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import ProjectService from '@/lib/application/services/projectService'
import { useRoute } from 'vue-router'
import mainLayout from '../../layouts/mainLayout.vue'
import { onMounted, ref } from 'vue'
import type { ProjectDataDTO } from '@/backend/BaseApi'

const route = useRoute()

const projectId = String(route.params.id)

const projectService = new ProjectService()
const project = ref<ProjectDataDTO | undefined>()

onMounted(async () => {
  await fetchProjectDetails()
})

const fetchProjectDetails = async () => {
  project.value = await projectService.GetProject(projectId)
}
</script>

<style scoped>
.project-detail-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 30px;
  background-color: #f9f9f9;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.project-title {
  font-size: 2rem;
  font-weight: bold;
  color: #2c3e50;
  margin-bottom: 20px;
}

.project-description {
  font-size: 1.1rem;
  color: #7f8c8d;
  line-height: 1.6;
  background-color: #fff;
  padding: 15px;
  border-radius: 8px;
  border: 1px solid #ddd;
  box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.05);
}

@media (max-width: 600px) {
  .project-detail-container {
    padding: 20px;
  }

  .project-title {
    font-size: 1.5rem;
  }

  .project-description {
    font-size: 1rem;
  }
}
</style>
