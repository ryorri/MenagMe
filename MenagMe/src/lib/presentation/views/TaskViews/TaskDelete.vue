<template>
  <mainLayout>
    <div class="project-details">
      <h2 class="project-name">{{ task?.name }}</h2>
      <p class="project-description">{{ task?.desc }}</p>

      <div class="confirmation-box">
        <h3>Are you sure?</h3>
        <strong>
          <button @click="deleteTask(taskId)" class="delete-btn">Delete!</button>
        </strong>
      </div>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import mainLayout from '../../layouts/mainLayout.vue'
import TaskService from '@/lib/application/services/taskService'

const route = useRoute()
const router = useRouter()

const storyId = Number(route.params.storyId)
const taskId = Number(route.params.taskId)

const taskService = new TaskService()

const task = taskService.Details(taskId, storyId)

const deleteTask = (id: number) => {
  taskService.Delete(id, storyId)
  router.push({ name: 'StoryList' })
}
</script>

<style scoped>
.project-details {
  max-width: 800px;
  margin: 50px auto;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 10px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
  text-align: center;
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
  margin-bottom: 30px;
  line-height: 1.6;
}

.confirmation-box {
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  border: 1px solid #ddd;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
  display: inline-block;
}

.confirmation-box h3 {
  font-size: 1.5rem;
  color: #e74c3c;
  font-weight: 600;
  margin-bottom: 15px;
}

.delete-btn {
  padding: 10px 20px;
  background-color: #e74c3c;
  color: white;
  font-size: 1.2rem;
  font-weight: bold;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.delete-btn:hover {
  background-color: #c0392b;
}

.delete-btn:focus {
  outline: none;
}

.delete-btn:active {
  transform: scale(0.98);
}
</style>
