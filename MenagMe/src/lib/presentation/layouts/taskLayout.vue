<template>
  <div class="d-flex flex-grow-1">
    <!-- Main -->
    <main :class="['p-4 flex-grow-1', themeStore.isDarkMode ? 'dark-mode' : 'bg-light']">
      <div><slot name="createbutton" /></div>
      <table :class="['table table-bordered', themeStore.isDarkMode ? 'table-dark' : '']">
        <thead>
          <tr>
            <th scope="col" class="fixed-width-col">To Do</th>
            <th scope="col" class="fixed-width-col">In progress</th>
            <th scope="col" class="fixed-width-col">Done</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>
              <slot name="todo" />
            </td>
            <td><slot name="inProgress" /></td>
            <td><slot name="done" /></td>
          </tr>
        </tbody>
      </table>
    </main>
  </div>
</template>

<script setup lang="ts">
import { useThemeStore } from '@/lib/application/stores/theme'

const themeStore = useThemeStore()
</script>

<style scoped>
.fixed-width-col {
  width: 33%;
}

.table {
  width: 100%;
  margin-top: 20px;
  border-radius: 8px;
  border-color: #dee2e6;
}

.table th {
  text-align: center;
}

.table-striped tbody tr:nth-of-type(odd) {
  background-color: #f8f9fa;
}

.table th,
.table td {
  padding: 12px 15px;
  text-align: center;
}

.table-bordered {
  border: 1px solid #dee2e6;
}

.table-dark {
  background-color: #2c2c2c;
  color: #ffffff;
  border-color: #dee2e6;
}

.table-dark th {
  background-color: #1d2124;
  color: #ffffff;
}

.table-dark td {
  background-color: #2c2c2c;
  color: #ffffff;
}
</style>
