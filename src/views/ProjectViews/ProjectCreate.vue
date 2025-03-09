<template>
  <form @submit.prevent="createProject" class="space-y-4 p-4 border rounded-lg">
    <div>
      <label for="name">Nazwa:</label>
      <input id="name" v-model="data.name" type="text" required />
    </div>

    <div>
      <label for="desc">Opis:</label>
      <textarea id="desc" v-model="data.desc" required></textarea>
    </div>

    <button type="submit">Create project</button>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import ProjectService from '@/lib/application/services/projectService'

const data = ref({ name: '', desc: '' })
const service = new ProjectService()

const createProject = () => {
  service.Create(data.value.name, data.value.desc)
  data.value = { name: '', desc: '' }
}
</script>
