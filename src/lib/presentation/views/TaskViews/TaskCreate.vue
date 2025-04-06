<template>
  <mainLayout>
    <div class="form-container">
      <h2>Create Story</h2>

      <form @submit.prevent="createTask">
        <div class="form-group">
          <label for="name">Task Name</label>
          <input v-model="data.name" id="name" class="form-control" required />
        </div>

        <div class="form-group">
          <label for="desc">Description</label>
          <textarea v-model="data.desc" id="desc" class="form-control" required></textarea>
        </div>

        <div class="form-group">
          <label for="prio">Priority</label>
          <select v-model="data.prio" id="prio" class="form-control">
            <option v-for="priority in priorities" :key="priority" :value="priority">
              {{ priority }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label for="state">State</label>
          <select v-model="data.state" id="state" class="form-control">
            <option v-for="state in states" :key="state" :value="state">
              {{ state }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label for="estTime">Estimated Time</label>
          <input v-model="data.estTime" type="date" id="estTime" class="form-control" required />
        </div>
        <button type="submit">Create Task</button>
      </form>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import { useRoute, useRouter } from 'vue-router'
import StateEnum from '@/lib/domain/enums/state'
import PriorityEnum from '@/lib/domain/enums/priority'
import UserService from '@/lib/application/services/userService'
import StoryService from '@/lib/application/services/storyService'
import TaskService from '@/lib/application/services/taskService'
import type { StoryInterface } from '@/lib/domain/interfaces/storyInterface'
import type { UserInterface } from '@/lib/domain/interfaces/userInterface'

const router = useRouter()
const route = useRoute()

const projectId = Number(route.params.projectId)
const storyId = Number(route.params.storyId)

const userService = new UserService()
const storyService = new StoryService()
const taskService = new TaskService()

const priorities = Object.values(PriorityEnum)
const states = Object.values(StateEnum)
const selectedStory = storyService.GetStory(storyId, projectId) as StoryInterface
const currentUser = userService.GetCurrentUser() as UserInterface

const data = ref({
  name: '',
  desc: '',
  prio: PriorityEnum.low,
  estTime: new Date(),
  state: StateEnum.todo,
  signedUser: null,
})

const createTask = () => {
  taskService.Create(
    data.value.name,
    data.value.desc,
    selectedStory,
    data.value.prio,
    data.value.estTime,
    data.value.state,
    null,
    null,
    data.value.signedUser,
    currentUser,
  )
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
</style>
