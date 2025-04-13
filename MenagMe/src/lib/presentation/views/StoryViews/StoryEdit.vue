<template>
  <mainLayout>
    <div class="form-container">
      <form @submit.prevent="editStory">
        <div class="form-group">
          <label for="name">Name</label>
          <input id="name" v-model="storyData.name" type="text" class="form-control" required />
        </div>

        <div class="form-group">
          <label for="desc">Description</label>
          <textarea id="desc" v-model="storyData.desc" class="form-control" required></textarea>
        </div>

        <div class="form-group">
          <label for="prio">Priority</label>
          <select id="prio" v-model="storyData.prio" class="form-control" required>
            <option v-for="(label, key) in PriorityEnum" :key="key" :value="key">
              {{ label }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label for="state">State</label>
          <select id="state" v-model="storyData.state" class="form-control" required>
            <option v-for="(label, key) in StateEnum" :key="key" :value="key">
              {{ label }}
            </option>
          </select>
        </div>

        <button type="submit" class="submit-btn">Edit STORY</button>
      </form>
    </div>
  </mainLayout>
</template>

<script setup lang="ts">
import StateEnum from '@/lib/domain/enums/state'
import PriorityEnum from '@/lib/domain/enums/priority'

import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import StoryService from '@/lib/application/services/storyService'
import ProjectService from '@/lib/application/services/projectService'
import UserService from '@/lib/application/services/userService'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'

const route = useRoute()
const router = useRouter()
const storyService = new StoryService()
const projectService = new ProjectService()
const userService = new UserService()

const storyId = Number(route.params.storyId)
const projectId = Number(route.params.projectId)

const storyData = ref({
  name: '',
  desc: '',
  prio: PriorityEnum.low,
  state: StateEnum.todo,
})

const currentProject = projectService.GetSelectProject()
const currentUser = userService.GetCurrentUser()
const story = storyService.Details(storyId, projectId)

onMounted(() => {
  if (story) {
    storyData.value = {
      name: story.name,
      desc: story.desc,
      prio: story.prio,
      state: story.state,
    }
  }
})

const editStory = () => {
  storyService.Edit(
    storyId,
    storyData.value.name,
    storyData.value.desc,
    storyData.value.prio,
    currentProject!,
    story!.createdAt,
    storyData.value.state,
    currentUser!,
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
