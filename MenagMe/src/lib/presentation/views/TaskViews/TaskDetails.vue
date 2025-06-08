<template>
  <mainLayout>
    <div class="task-details" v-if="task">
      <h2 class="task-title">{{ task?.name }}</h2>
      <p class="task-desc">{{ task?.description }}</p>

      <div class="task-info">
        <p><strong>Priority:</strong> {{ task?.priority }}</p>
        <p><strong>State:</strong> {{ task?.status }}</p>
        <p><strong>Created At:</strong> {{ formatDate(task?.createdAt) }}</p>
        <p><strong>Story:</strong> {{ story?.name }}</p>
        <p><strong>Estimated date:</strong> {{ formatDate(task?.estimatedTime) }}</p>
        <p><strong>Start date:</strong> {{ formatDate(task?.startDate) }}</p>
        <p><strong>End date:</strong> {{ formatDate(task?.endDate) }}</p>
        <p>
          <strong>Man hours:</strong>
          {{ manHours(dateToTimestamp(task?.endDate), dateToTimestamp(task?.startDate)) }}
        </p>
        <p v-if="author">
          <strong>Autor:</strong> {{ author.name }} {{ author.surname }} ({{ author.role }})
        </p>
        <p v-if="user">
          <strong>Assigned user :</strong> {{ user.name }} {{ user.surname }} ({{ user.role }})
        </p>
      </div>
    </div>
    <div v-else>
      <p>Loading task details...</p>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import TaskService from '@/lib/application/services/taskService'
import { formatDate, dateToTimestamp } from '@/lib/application/extensions/dateFormatter'
import { onMounted, ref } from 'vue'
import StoryService from '@/lib/application/services/storyService'
import UserService from '@/lib/application/services/userService'
const route = useRoute()
const taskService = new TaskService()
const storyService = new StoryService()
const userService = new UserService()

const storyId = String(route.params.storyId)
const taskId = String(route.params.taskId)

const task = ref()
const story = ref()
const author = ref()
const user = ref()

const manHours = (end: number, start: number) => {
  const differenceInMilliseconds = end - start

  if (differenceInMilliseconds < 0) {
    return 0
  }
  const differenceInHours = differenceInMilliseconds / (1000 * 60 * 60)
  return parseFloat(differenceInHours.toFixed(2))
}

onMounted(async () => {
  task.value = await taskService.GetTask(taskId)
  story.value = await storyService.GetStory(storyId)
  author.value = await userService.GetUser(task.value.userId)
  user.value = await userService.GetUser(task.value.assignedUserId)
})
</script>

<style scoped>
.task-details {
  max-width: 700px;
  margin: 50px auto;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.task-title {
  font-size: 2rem;
  font-weight: bold;
  color: #2c3e50;
}

.task-desc {
  font-size: 1.2rem;
  color: #7f8c8d;
  margin-bottom: 20px;
}

.task-info p {
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
