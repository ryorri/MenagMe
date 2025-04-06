<template>
  <mainLayout>
    <div class="task-details">
      <h2 class="task-title">{{ task?.name }}</h2>
      <p class="task-desc">{{ task?.desc }}</p>

      <div class="task-info">
        <p><strong>Priority:</strong> {{ task?.prio }}</p>
        <p><strong>State:</strong> {{ task?.state }}</p>
        <p><strong>Created At:</strong> {{ task?.createdAt }}</p>
        <p><strong>Assigned user :</strong> {{ task?.signedUser?.name }}</p>
      </div>
    </div>

    <form @submit.prevent="changeState">
      <div class="form-group">
        <label for="state">State</label>
        <select v-model="data.state" id="state" class="form-control">
          <option v-for="state in states" :key="state" :value="state">
            {{ state }}
          </option>
        </select>
      </div>
      <button type="submit">Change state</button>
    </form>
  </mainLayout>
</template>

<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import TaskService from '@/lib/application/services/taskService'
import StateEnum from '@/lib/domain/enums/state'
import { ref } from 'vue'

const router = useRouter()
const route = useRoute()
const taskService = new TaskService()

const projectId = Number(route.params.projectId)
const storyId = Number(route.params.storyId)
const taskId = Number(route.params.taskId)

const task = taskService.Details(taskId, storyId)
const states = Object.values(StateEnum)

const data = ref({
  state: StateEnum.todo,
})

const changeState = () => {
  taskService.ChangeState(task!, data.value.state)
  router.push({ name: 'StoryList', params: { projectId: projectId } })
}
</script>

<style scoped>
.form-container {
  max-width: 500px;
  margin: 0 auto;
  padding: 30px;
  background-color: #f8f9fa;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.form-group {
  margin-bottom: 20px;
}

label {
  font-weight: bold;
  display: block;
  margin-bottom: 8px;
  font-size: 14px;
  color: #333;
}

.form-control {
  width: 100%;
  padding: 10px;
  border-radius: 5px;
  border: 1px solid #ddd;
  font-size: 14px;
  background-color: #fff;
  transition: border-color 0.3s ease;
}

.form-control:focus {
  border-color: #28a745;
  outline: none;
}

button[type='submit'] {
  padding: 12px 25px;
  font-size: 16px;
  font-weight: bold;
  background-color: #28a745;
  color: white;
  border: none;
  border-radius: 5px;
  transition:
    background-color 0.3s ease,
    transform 0.2s ease;
}

button[type='submit']:hover {
  background-color: #218838;
  transform: scale(1.05);
}

button[type='submit']:focus {
  outline: none;
  box-shadow: 0 0 10px rgba(33, 136, 56, 0.7);
}
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
