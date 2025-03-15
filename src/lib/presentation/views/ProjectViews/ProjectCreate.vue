<template>
  <mainLayout>
    <form @submit.prevent="createProject" class="form-container">
      <!-- Nazwa -->
      <div class="form-group">
        <label for="name">Name:</label>
        <input
          id="name"
          v-model="data.name"
          type="text"
          class="form-control"
          placeholder="Enter project name"
          required
        />
      </div>

      <!-- Opis -->
      <div class="form-group">
        <label for="desc">Description:</label>
        <textarea
          id="desc"
          v-model="data.desc"
          class="form-control"
          placeholder="Enter project description"
          required
        ></textarea>
      </div>

      <!-- Przycisk do utworzenia projektu -->
      <button type="submit" class="btn btn-success">Create project</button>
    </form>
  </mainLayout>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import ProjectService from '@/lib/application/services/projectService'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import { useRouter } from 'vue-router'

const data = ref({ name: '', desc: '' })
const service = new ProjectService()
const router = useRouter()

const createProject = () => {
  service.Create(data.value.name, data.value.desc)
  data.value = { name: '', desc: '' }
  router.push({ name: 'ProjectList' })
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
