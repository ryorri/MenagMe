<template>
  <mainLayout>
    <div v-if="!loading">
      <p>Loading...</p>
    </div>
    <div v-else class="story-details">
      <h2 class="story-title">{{ story?.name }}</h2>
      <p class="story-desc">{{ story?.description }}</p>

      <div class="story-info">
        <p><strong>Priority:</strong> {{ story?.priority }}</p>
        <p><strong>State:</strong> {{ story?.status }}</p>
        <p><strong>Created At:</strong> {{ formatDate(story?.createdAt) }}</p>
        <p><strong>Project:</strong> {{ project?.name }}</p>
        <p><strong>Author:</strong> {{ user?.name }} {{ user?.surname }}</p>
      </div>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import StoryService from '@/lib/application/services/storyService'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import { formatDate } from '@/lib/application/extensions/dateFormatter'
import type { ProjectDataDTO, StoryDataDTO, UserDataDTO } from '@/backend/BaseApi'
import { onMounted, ref } from 'vue'
import ProjectService from '@/lib/application/services/projectService'
import UserService from '@/lib/application/services/userService'

const route = useRoute()
const storyService = new StoryService()
const projectService = new ProjectService()
const userService = new UserService()

const storyId = String(route.params.storyId)
const projectId = String(route.params.projectId)

const story = ref<StoryDataDTO>()
const project = ref<ProjectDataDTO>()
const user = ref<UserDataDTO | undefined>()
const loading = ref(false)

onMounted(async () => {
  await fetchStoryData()
  await fetchProjectData()
  await fetchUserData()
  loading.value = true
})

const fetchStoryData = async () => {
  story.value = await storyService.GetStory(storyId)
}
const fetchProjectData = async () => {
  project.value = await projectService.GetProject(projectId)
}
const fetchUserData = async () => {
  const userId = story.value!.userId
  if (userId) {
    user.value = await userService.GetUser(userId)
  }
}
</script>

<style scoped>
.story-details {
  max-width: 700px;
  margin: 50px auto;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.story-title {
  font-size: 2rem;
  font-weight: bold;
  color: #2c3e50;
}

.story-desc {
  font-size: 1.2rem;
  color: #7f8c8d;
  margin-bottom: 20px;
}

.story-info p {
  font-size: 1rem;
  color: #333;
  margin: 5px 0;
}

.loading {
  text-align: center;
  font-size: 1.5rem;
  color: #666;
}

.back-btn {
  margin-top: 20px;
  padding: 10px 20px;
  background-color: #3498db;
  color: white;
  font-size: 1rem;
  font-weight: bold;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.back-btn:hover {
  background-color: #2980b9;
}
</style>
