<template>
  <mainLayout>
    <div class="form-container">
      <h2>Create Story</h2>

      <form @submit.prevent="createStory">
        <div class="form-group">
          <label for="name">Story Name</label>
          <input v-model="data.name" id="name" class="form-control" required />
        </div>

        <div class="form-group">
          <label for="desc">Description</label>
          <textarea v-model="data.description" id="desc" class="form-control" required></textarea>
        </div>

        <div class="form-group">
          <label for="prio">Priority</label>
          <select v-model="data.priority" id="prio" class="form-control">
            <option v-for="priority in priorities" :key="priority" :value="priority">
              {{ priority }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label for="state">State</label>
          <select v-model="data.status" id="state" class="form-control">
            <option v-for="state in states" :key="state" :value="state">
              {{ state }}
            </option>
          </select>
        </div>

        <button type="submit">Create Story</button>
      </form>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import StoryService from '@/lib/application/services/storyService'
import ProjectService from '@/lib/application/services/projectService'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import { useRoute, useRouter } from 'vue-router'
import UserService from '@/lib/application/services/userService'
import StateEnum from '@/lib/domain/enums/state'
import PriorityEnum from '@/lib/domain/enums/priority'
import type { StoryCreateDTO } from '@/backend/BaseApi'

const priorities = Object.values(PriorityEnum)
const states = Object.values(StateEnum)

const projectService = new ProjectService()
const storyService = new StoryService()
const userService = new UserService()
const router = useRouter()
const route = useRoute()

const projectId = String(route.params.projectId)

const data = ref<StoryCreateDTO>({
  name: '',
  description: '',
  priority: PriorityEnum.low,
  status: StateEnum.todo,
  userId: '',
  projectId: '',
})

onMounted(async () => {
  fetchProjectData()
})

const fetchProjectData = async () => {
  const currentUser = await userService.GetCurrentUser()
  const currentProject = projectService.GetSelectProject()

  data.value = {
    name: '',
    description: '',
    priority: PriorityEnum.low,
    status: StateEnum.todo,
    userId: currentUser?.id ?? '',
    projectId: currentProject?.id ?? '',
  }
}
const createStory = async () => {
  await storyService.CreateStory(data.value)
  console.log('Story created successfully:', data.value)

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
