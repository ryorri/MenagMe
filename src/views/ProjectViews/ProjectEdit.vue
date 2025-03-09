
<template>
  {{ project?.name }}
  {{ project?.desc }}
</br>
</br>
  <form @submit.prevent="editProject" class="space-y-4 p-4 border rounded-lg">
    <div>
      <label for="name">Nazwa:</label>
      <input id="name" v-model="editedProject.name" type="text" required />
    </div>

    <div>
      <label for="desc">Opis:</label>
      <textarea id="desc" v-model="editedProject.desc" required></textarea>
    </div>

    <button type="submit">Edit project</button>
  </form>
</template>

<script setup lang="ts">
import ProjectService from '@/lib/application/services/projectService'
import {ref} from 'vue'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()

const projectId = Number(route.params.id)

const projectService = new ProjectService()

const project = projectService.Details(projectId)

const editedProject =ref({ name: '', desc: '' })

const editProject = () =>{
    projectService.Edit(projectId, editedProject.value.name, editedProject.value.desc )
    router.push({ name: 'ProjectList' })
}
</script>
