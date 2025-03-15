<template>
  <div class="d-flex flex-column vh-100">
    <!-- Header -->
    <header
      class="bg-success text-white p-3 d-flex justify-content-between align-items-center shadow"
    >
      <!--Login-->
      <div v-if="user" class="user-box d-flex align-items-center">
        <span>{{ user }}</span>
        <button class="btn btn-outline-light btn-sm log-out-btn">Log out</button>
      </div>
      <h2 class="m-0 text-center flex-grow-1">MenagMe</h2>
    </header>

    <div class="d-flex flex-grow-1">
      <!-- Sidebar -->
      <nav class="bg-light border-end p-3 sidebar">
        <ul class="nav flex-column">
          <li class="nav-item">
            <router-link to="/project/list" class="nav-link text-success">📂 Projects</router-link>
          </li>
        </ul>
      </nav>

      <!-- Main -->
      <main class="p-4 flex-grow-1 bg-light">
        <slot />
      </main>
    </div>

    <!-- Footer -->
    <footer class="bg-success text-white text-center p-2">
      <p>&copy; 2025 MenagMe</p>
    </footer>
  </div>
</template>

<style scoped>
.sidebar {
  width: 250px;
}

.nav-link {
  font-weight: bold;
}

.nav-link:hover {
  background-color: #28a745;
  color: white !important;
  border-radius: 5px;
}

.user-box {
  background-color: rgba(255, 255, 255, 0.2);
  border: 2px solid white;
  border-radius: 10px;
  padding: 5px 10px;
  font-weight: bold;
  box-shadow: 0 0 5px rgba(255, 255, 255, 0.3);
  width: 10vw;
  height: 8vh;
}
</style>

<script setup lang="ts">
import UserService from '@/lib/application/services/userService'
import { onMounted } from 'vue'

const userService = new UserService()
const user = userService.GetCurrentUser()
</script>
