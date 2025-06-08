<template>
  <mainLayout>
    <div class="form-container">
      <form @submit.prevent="editTask">
        <div class="form-group">
          <label for="name">Name</label>
          <input id="name" v-model="editedTask.name" type="text" class="form-control" required />
        </div>

        <div class="form-group">
          <label for="desc">Description</label>
          <textarea
            id="desc"
            v-model="editedTask.description"
            class="form-control"
            required
          ></textarea>
        </div>

        <div class="form-group">
          <label for="prio">Priority</label>
          <select v-model="editedTask.priority" id="prio" class="form-control">
            <option v-for="priority in priorities" :key="priority" :value="priority">
              {{ priority }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label for="estTime">Estimated Time</label>
          <input id="estTime" type="date" v-model="editedTask.estimatedTime" class="form-control" />
        </div>

        <button type="submit" class="submit-btn">Edit TASK</button>
      </form>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import StateEnum from '@/lib/domain/enums/state'
import PriorityEnum from '@/lib/domain/enums/priority'

import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import TaskService from '@/lib/application/services/taskService'
import type { TasksDataDTO } from '@/backend/BaseApi'

const route = useRoute()
const router = useRouter()
const taskService = new TaskService()

const storyId = String(route.params.storyId)
const projectId = String(route.params.projectId)
const taskId = String(route.params.taskId)

const priorities = Object.values(PriorityEnum)
const states = Object.values(StateEnum)

const editedTask = ref({
  id: '',
  name: '',
  description: '',
  priority: '',
  status: '',
  createdAt: '',
  userId: '',
  storyId: '',
  estimatedTime: '',
  startDate: '',
  endDate: '',
  assignedUserId: '',
})

onMounted(async () => {
  const task = await taskService.GetTask(taskId)
  if (task) {
    editedTask.value = {
      id: task?.id || '',
      name: task?.name || '',
      description: task?.description || '',
      priority: task?.priority || PriorityEnum.low,
      status: task?.status || StateEnum.todo,
      estimatedTime: task?.estimatedTime ? String(task.estimatedTime) : String(Date.now()),
      userId: task?.userId || '',
      storyId: task?.storyId || '',
      createdAt: task?.createdAt ? String(task.createdAt) : String(Date.now()),
      startDate: task?.startDate ? String(task.startDate) : '',
      endDate: task?.endDate ? String(task.endDate) : '',
      assignedUserId: task?.assignedUserId || '',
    }
  }
})

const editTask = async () => {
  // Convert fields to correct types for TasksDataDTO
  const updatedTask: TasksDataDTO = {
    ...editedTask.value,
    createdAt: new Date(editedTask.value.createdAt),
    estimatedTime: new Date(editedTask.value.estimatedTime),
    startDate: editedTask.value.startDate ? new Date(editedTask.value.startDate) : undefined,
    endDate: editedTask.value.endDate ? new Date(editedTask.value.endDate) : undefined,
  }
  await taskService.UpdateTask(taskId, updatedTask)
  router.push({ name: 'StoryList', params: { id: projectId } })
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

.submit-btn {
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

.submit-btn:hover {
  background-color: #218838;
  transform: scale(1.05);
}
</style>
