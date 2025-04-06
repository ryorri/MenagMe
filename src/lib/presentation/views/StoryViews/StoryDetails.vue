<template>
  <mainLayout>
    <div class="story-details">
      <h2 class="story-title">{{ story?.name }}</h2>
      <p class="story-desc">{{ story?.desc }}</p>

      <div class="story-info">
        <p><strong>Priority:</strong> {{ story?.prio }}</p>
        <p><strong>State:</strong> {{ story?.state }}</p>
        <p><strong>Created At:</strong> {{ formatDate(story?.createdAt) }}</p>
        <p><strong>Project:</strong> {{ story?.project.name }}</p>
        <p><strong>Author:</strong> {{ story?.user.name }}</p>
      </div>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import StoryService from '@/lib/application/services/storyService'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import { formatDate } from '@/lib/application/extensions/dateFormatter'

const route = useRoute()
const storyService = new StoryService()

const storyId = Number(route.params.storyId)
const projectId = Number(route.params.projectId)

const story = storyService.Details(storyId, projectId)
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
