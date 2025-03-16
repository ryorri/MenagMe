<template>
  <mainLayout>
    <button class="btn btn-success mb-4 create-project-btn" @click="goToCreateStory">
      Create STORY
    </button>

    <table class="table table-striped table-bordered">
      <thead class="table-success">
        <tr>
          <th>Name</th>
          <th>Description</th>
          <th>Details</th>
          <th>Edit</th>
          <th>Delete</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="story in storiesList" :key="story.id">
          <td>{{ story.name }}</td>
          <td>{{ story.desc }}</td>
          <td>
            <button class="btn btn-info" @click="goToDetailsStory(story.id)">Show details</button>
          </td>
          <td>
            <button class="btn btn-warning" @click="goToEditStory(story.id)">Edit project</button>
          </td>
          <td>
            <button class="btn btn-danger" @click="goToDeleteStory(story.id)">
              Delete project
            </button>
          </td>
        </tr>
      </tbody>
    </table></mainLayout
  >
</template>

<script setup lang="ts">
import type { StoryInterface } from '@/lib/domain/interfaces/storyInterface'
import { useRoute, useRouter } from 'vue-router'
import mainLayout from '../../layouts/mainLayout.vue'
import StoryService from '@/lib/application/services/storyService'
import { ref, onMounted } from 'vue'

const route = useRoute()
const router = useRouter()

const projectId = Number(route.params.projectId)

const storiesService = new StoryService()

const storiesList = ref<StoryInterface[] | null>([])

const goToCreateStory = () => {
  console.log(projectId)
  router.push({ name: 'StoryCreate', params: { projectId: projectId } })
}
const goToEditStory = (_storyid: number) => {
  router.push({ name: 'StoryEdit', params: { projectId: projectId, storyId: _storyid } })
}
const goToDeleteStory = (_storyid: number) => {
  router.push({ name: 'StoryDelete', params: { projectId: projectId, storyId: _storyid } })
}
const goToDetailsStory = (_storyid: number) => {
  router.push({ name: 'StoryDetails', params: { projectId: projectId, storyId: _storyid } })
}

onMounted(() => {
  fetchStoriesList()
  console.log(storiesList)
})

const fetchStoriesList = () => {
  storiesList.value = storiesService.GetStoriesList(projectId)
}
</script>
