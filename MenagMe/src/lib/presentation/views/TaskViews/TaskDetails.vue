<template>
  <mainLayout>
    <div class="task-details">
      <h2 class="task-title">{{ task?.name }}</h2>
      <p class="task-desc">{{ task?.desc }}</p>

      <div class="task-info">
        <p><strong>Priority:</strong> {{ task?.prio }}</p>
        <p><strong>State:</strong> {{ task?.state }}</p>
        <p><strong>Created At:</strong> {{ formatDate(task?.createdAt) }}</p>
        <p><strong>Story:</strong> {{ task?.story.name }}</p>
        <p><strong>Estimated date:</strong> {{ formatDate(task?.estimatedTime) }}</p>
        <p><strong>Start date:</strong> {{ formatDate(task?.startDate) }}</p>
        <p><strong>End date:</strong> {{ formatDate(task?.endDate) }}</p>
        <p>
          <strong>Man hours:</strong>
          {{ manHours(dateToTimestamp(task?.endDate), dateToTimestamp(task?.startDate)) }}
        </p>
        <p><strong>Autor:</strong> {{ task?.user?.name }}</p>
        <p><strong>Assigned user :</strong> {{ task?.signedUser?.name }}</p>
      </div>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import TaskService from '@/lib/application/services/taskService'
import { formatDate, dateToTimestamp } from '@/lib/application/extensions/dateFormatter'
const route = useRoute()
const taskService = new TaskService()

const storyId = Number(route.params.storyId)
const taskId = Number(route.params.taskId)

const task = taskService.Details(taskId, storyId)

const manHours = (end: number, start: number) => {
  const differenceInMilliseconds = end - start

  if (differenceInMilliseconds < 0) {
    return 0
  }
  const differenceInHours = differenceInMilliseconds / (1000 * 60 * 60)
  return parseFloat(differenceInHours.toFixed(2))
}
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
