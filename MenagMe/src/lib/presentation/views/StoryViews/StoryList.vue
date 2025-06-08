<template>
  <mainLayout>
    <button
      class="btn mb-4 create-project-btn"
      :class="themeStore.isDarkMode ? 'btn-dark' : 'btn-success'"
      @click="goToCreateStory"
    >
      Create STORY
    </button>

    <div v-if="!storiesList">Loading...</div>
    <table
      v-else
      :class="[
        'table table-striped table-bordered',
        themeStore.isDarkMode ? 'table-dark' : 'table-light',
      ]"
      v-for="story in storiesList"
      :key="story.id"
    >
      <thead class="table-success">
        <tr>
          <th>Name</th>
          <th>Description</th>
          <th>Status</th>
          <th>Priority</th>
          <th>Details</th>
          <th>Edit</th>
          <th>Delete</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>{{ story.name }}</td>
          <td>{{ story.description }}</td>
          <td>{{ story.status }}</td>
          <td>{{ story.priority }}</td>
          <td>
            <button class="btn btn-info" @click="goToDetailsStory(story.id!)">Show details</button>
          </td>
          <td>
            <button class="btn btn-warning" @click="goToEditStory(story.id!)">Edit story</button>
          </td>
          <td>
            <button class="btn btn-danger" @click="goToDeleteStory(story.id!)">Delete story</button>
          </td>
        </tr>
        <tr class="toggle-arrow" @click="toggleDetails(story.id!)">
          <td colspan="7">▼</td>
        </tr>
        <tr v-if="storyId === story.id">
          <td colspan="7">
            <taskLayout>
              <!--TaskList-->
              <template #createbutton
                ><button
                  class="btn mb-4 create-project-btn"
                  :class="themeStore.isDarkMode ? 'btn-dark' : 'btn-success'"
                  @click="goToCreateTask(storyId.value)"
                >
                  Create TASK
                </button></template
              >
              <template #todo>
                <div v-for="task in getTaskListByState('ToDo')" :key="task.id" class="task-item">
                  <div class="task-name">{{ task.name }}</div>
                  <div class="task-buttons">
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToDetailsTask(task.id || '', story.id || '')"
                    >
                      Details
                    </button>
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToEditTask(task.id || '', story.id || '')"
                    >
                      Edit task
                    </button>
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToAssignUserChangeStateTask(task.id || '', story.id || '')"
                    >
                      Assign user & edit state
                    </button>
                    <button
                      class="btn btn-danger"
                      @click="goToDeleteTask(task.id || '', story.id || '')"
                    >
                      Delete task
                    </button>
                  </div>
                </div>
              </template>
              <template #inProgress>
                <div
                  v-for="task in getTaskListByState('InProgress')"
                  :key="task.id"
                  class="task-item"
                >
                  <div class="task-name">{{ task.name }}</div>
                  <div class="task-buttons">
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToDetailsTask(task.id || '', story.id || '')"
                    >
                      Details
                    </button>
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToEditTask(task.id || '', story.id || '')"
                    >
                      Edit task
                    </button>
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToChangeStateTask(task.id || '', story.id || '')"
                    >
                      Edit state
                    </button>
                    <button
                      class="btn btn-danger"
                      @click="goToDeleteTask(task.id || '', story.id || '')"
                    >
                      Delete task
                    </button>
                  </div>
                </div>
              </template>

              <template #done>
                <div v-for="task in getTaskListByState('Done')" :key="task.id" class="task-item">
                  <div class="task-name">{{ task.name }}</div>
                  <div class="task-buttons">
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToDetailsTask(task.id || '', story.id || '')"
                    >
                      Details
                    </button>
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToChangeStateTask(task.id || '', story.id || '')"
                    >
                      Edit state
                    </button>
                    <button
                      class="btn btn-danger"
                      @click="goToDeleteTask(task.id || '', story.id || '')"
                    >
                      Delete task
                    </button>
                  </div>
                </div>
              </template> </taskLayout
            ><!--end of TaskList-->
          </td>
        </tr>
      </tbody>
    </table>
  </mainLayout>
</template>

<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import StoryService from '@/lib/application/services/storyService'
import { ref, onMounted } from 'vue'
import taskLayout from '@/lib/presentation/layouts/taskLayout.vue'
import TaskService from '@/lib/application/services/taskService'
import { useThemeStore } from '@/lib/application/stores/theme'
import type { StoryDataDTO, TasksDataDTO } from '@/backend/BaseApi'

const route = useRoute()
const router = useRouter()

const projectId = String(route.params.projectId) || ''
const storyId = ref()

const storiesService = new StoryService()
const taskService = new TaskService()

const storiesList = ref<StoryDataDTO[] | undefined>([])
const tasksList = ref<TasksDataDTO[] | undefined>([])

const themeStore = useThemeStore()

const goToCreateStory = () => {
  router.push({ name: 'StoryCreate', params: { projectId: projectId } })
}
const goToEditStory = (_storyid: string) => {
  router.push({ name: 'StoryEdit', params: { projectId: projectId, storyId: _storyid } })
}
const goToDeleteStory = (_storyid: string) => {
  router.push({ name: 'StoryDelete', params: { projectId: projectId, storyId: _storyid } })
}
const goToDetailsStory = (_storyid: string) => {
  router.push({ name: 'StoryDetails', params: { projectId: projectId, storyId: _storyid } })
}
const goToCreateTask = (_storyid: string) => {
  router.push({ name: 'TaskCreate', params: { projectId: projectId, storyId: storyId.value } })
}
const goToDeleteTask = (taskId: string, storyId: string) => {
  router.push({
    name: 'TaskDelete',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}
const goToDetailsTask = (taskId: string, storyId: string) => {
  router.push({
    name: 'TaskDetails',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}
const goToEditTask = (taskId: string, storyId: string) => {
  router.push({
    name: 'TaskEdit',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}

const goToChangeStateTask = (taskId: string, storyId: string) => {
  router.push({
    name: 'TaskChangeState',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}

const goToAssignUserChangeStateTask = (taskId: string, storyId: string) => {
  router.push({
    name: 'TaskChangeStateAssignUser',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}

onMounted(() => {
  fetchStoriesList()
})

const fetchStoriesList = async () => {
  storiesList.value = await storiesService.GetStoryListByProject(projectId)
}
const toggleDetails = async (Id: string) => {
  storyId.value = Id
  await fetchTasksList(Id)
  console.log(tasksList)
}

const fetchTasksList = async (storyId: string) =>
  (tasksList.value = await taskService.GetSpecificTaskListByStory(storyId))

const getTaskListByState = (state: string) => {
  if (tasksList) return tasksList.value?.filter((task) => task.status === state) || []
  console.log(tasksList)
}
</script>

<style scoped>
.toggle-arrow {
  background-color: #f8f9fa;
  text-align: center;
  cursor: pointer;
}
.task-item {
  background-color: #ffffff;
  border: 1px solid #dee2e6;
  border-radius: 8px;
  padding: 15px;
  margin-bottom: 15px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
}

.dark-mode .task-item {
  background-color: #23272b;
  border: 1px solid #444950;
  color: #f1f1f1;
}

.task-name {
  font-weight: bold;
  margin-bottom: 10px;
  font-size: 16px;
}

.dark-mode .task-name {
  color: #f1f1f1;
}

.task-buttons {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}
.task-buttons {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 10px;
}

.task-buttons button {
  width: 100%;
  text-align: center;
}

.dark-mode .task-buttons button {
  background-color: #343a40;
  color: #f1f1f1;
  border-color: #444950;
}

.dark-mode .task-buttons button.btn-danger {
  background-color: #c82333;
  border-color: #c82333;
  color: #fff;
}

.dark-mode .task-buttons button.btn-secondary {
  background-color: #495057;
  border-color: #495057;
  color: #fff;
}

/* Adjust buttons in dark mode */
.btn-info {
  background-color: #17a2b8;
  border-color: #17a2b8;
  color: white;
}

.btn-info:hover {
  background-color: #138496;
  border-color: #138496;
}

.btn-warning {
  background-color: #ffc107;
  border-color: #ffc107;
  color: black;
}

.btn-warning:hover {
  background-color: #e0a800;
  border-color: #e0a800;
}

.btn-danger {
  background-color: #dc3545;
  border-color: #dc3545;
  color: white;
}

.btn-danger:hover {
  background-color: #c82333;
  border-color: #c82333;
}

.btn-dark {
  background-color: #343a40;
  border-color: #343a40;
  color: white;
}

.btn-dark:hover {
  background-color: #23272b;
  border-color: #23272b;
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

.table-success {
  background-color: #28a745;
  color: white;
}

.table-success th {
  background-color: #28a745;
  color: white;
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

.btn-info {
  background-color: #007bff; /* Default color for 'Show details' button */
  border-color: #007bff;
  color: white;
}

.btn-info:hover {
  background-color: #0056b3;
  border-color: #0056b3;
}

.dark-mode .btn-info {
  background-color: #c8c9c7; /* Dark mode color for 'Show details' button */
  border-color: white;
  color: black;
}

.btn-warning {
  background-color: #ffcc00; /* Default color for 'Edit project' button */
  border-color: #ffcc00;
  color: black;
}

.btn-warning:hover {
  background-color: #e6b800;
  border-color: #e6b800;
}

.dark-mode .btn-warning {
  background-color: #c8c9c7; /* Dark mode color for 'Show details' button */
  border-color: white;
  color: black;
}

.btn-danger {
  background-color: #dc3545;
  border-color: #dc3545;
  color: white;
}

.btn-danger:hover {
  background-color: #c82333;
  border-color: #c82333;
}
</style>
