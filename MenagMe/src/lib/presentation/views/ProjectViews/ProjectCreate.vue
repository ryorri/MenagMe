<template>
  <mainLayout>
    <form @submit.prevent="createProject" class="form-container">
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

      <button
        type="submit"
        class="btn create-project-btn"
        :class="themeStore.isDarkMode ? 'btn-dark' : 'btn-success'"
      >
        Create project
      </button>
    </form>
  </mainLayout>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import ProjectService from '@/lib/application/services/projectService'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import { useRouter } from 'vue-router'
import { useThemeStore } from '@/lib/application/stores/theme'

const data = ref({ name: '', desc: '' })
const service = new ProjectService()
const router = useRouter()
const themeStore = useThemeStore()

// Initialize theme on component mount
themeStore.initializeTheme()

const createProject = () => {
  service.Create(data.value.name, data.value.desc)
  data.value = { name: '', desc: '' }
  router.push({ name: 'ProjectList' })
}
</script>

<style scoped>
.create-project-btn {
  padding: 10px 20px;
  font-weight: bold;
  font-size: 16px;
  border-radius: 5px;
}

.create-project-btn.btn-dark {
  background-color: #343a40; /* Darker background for dark mode */
  color: white;
  border: none;
}

.create-project-btn.btn-dark:hover {
  background-color: #23272b; /* Updated hover color for dark mode */
}

.create-project-btn.btn-success {
  background-color: #28a745;
  color: white;
  border: none;
}

.create-project-btn.btn-success:hover {
  background-color: #218838;
}

.create-project-btn:focus {
  outline: none;
  box-shadow: 0 0 10px rgba(33, 136, 56, 0.7);
}

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
</style>
