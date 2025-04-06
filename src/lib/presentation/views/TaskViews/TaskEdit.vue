<template>
  <mainLayout>
    <div class="form-container">
      <form @submit.prevent="editTask">
        <div class="form-group">
          <label for="name">Name</label>
          <input id="name" v-model="taskData.name" type="text" class="form-control" required />
        </div>

        <div class="form-group">
          <label for="desc">Description</label>
          <textarea id="desc" v-model="taskData.desc" class="form-control" required></textarea>
        </div>

        <div class="form-group">
          <label for="prio">Priority</label>
          <select v-model="taskData.prio" id="prio" class="form-control">
            <option v-for="priority in priorities" :key="priority" :value="priority">
              {{ priority }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label for="state">State</label>
          <select v-model="taskData.state" id="state" class="form-control">
            <option v-for="state in states" :key="state" :value="state">
              {{ state }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label for="estTime">Estimated Time</label>
          <input id="estTime" type="date" v-model="taskData.estTime" class="form-control" />
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

const route = useRoute()
const router = useRouter()
const taskService = new TaskService()

const storyId = Number(route.params.storyId)
const projectId = Number(route.params.projectId)
const taskId = Number(route.params.taskId)

const priorities = Object.values(PriorityEnum)
const states = Object.values(StateEnum)

const task = taskService.Details(taskId, storyId)

const taskData = ref({
  name: '',
  desc: '',
  prio: PriorityEnum.low,
  state: StateEnum.todo,
  estTime: task?.estimatedTime,
})

onMounted(() => {
  if (task) {
    taskData.value = {
      name: task.name,
      desc: task.desc,
      prio: task.prio,
      state: task.state,
      estTime: task.estimatedTime,
    }
  }
})

const editTask = () => {
  taskService.Edit(
    taskId,
    taskData.value.name,
    taskData.value.desc,
    task?.story!,
    taskData.value.prio,
    taskData.value.estTime!,
    taskData.value.state,
    task?.createdAt!,
    task?.startDate!,
    task?.endDate!,
    task?.signedUser!,
    task?.user!,
  )

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
