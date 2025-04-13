<template>
  <mainLayout>
    <button class="btn btn-success mb-4 create-project-btn" @click="goToCreateStory">
      Create STORY
    </button>

    <table class="table table-striped table-bordered" v-for="story in storiesList" :key="story.id">
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
        <tr>
          <td>{{ story.name }}</td>
          <td>{{ story.desc }}</td>
          <td>
            <button class="btn btn-info" @click="goToDetailsStory(story.id)">Show details</button>
          </td>
          <td>
            <button class="btn btn-warning" @click="goToEditStory(story.id)">Edit story</button>
          </td>
          <td>
            <button class="btn btn-danger" @click="goToDeleteStory(story.id)">Delete story</button>
          </td>
        </tr>
        <tr class="toggle-arrow" @click="toggleDetails(story.id)">
          <td colspan="5">▼</td>
        </tr>
        <tr v-if="toogleDetails === story.id">
          <td colspan="5">
            <taskLayout>
              <!--TaskList-->
              <template #createbutton
                ><button
                  class="btn btn-success mb-4 create-project-btn"
                  @click="goToCreateTask(story.id)"
                >
                  Create TASK
                </button></template
              >
              <template #todo>
                <div v-for="task in tasksList(story.id, 'TODO')" :key="task.id" class="task-item">
                  <div class="task-name">{{ task.name }}</div>
                  <div class="task-buttons">
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToDetailsTask(task.id, story.id)"
                    >
                      Details
                    </button>
                    <button class="btn btn-secondary me-2" @click="goToEditTask(task.id, story.id)">
                      Edit task
                    </button>
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToAssignUserChangeStateTask(task.id, story.id)"
                    >
                      Assign user & edit state
                    </button>
                    <button class="btn btn-danger" @click="goToDeleteTask(task.id, story.id)">
                      Delete task
                    </button>
                  </div>
                </div>
              </template>
              <template #inProgress>
                <div
                  v-for="task in tasksList(story.id, 'IN PROGRESS')"
                  :key="task.id"
                  class="task-item"
                >
                  <div class="task-name">{{ task.name }}</div>
                  <div class="task-buttons">
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToDetailsTask(task.id, story.id)"
                    >
                      Details
                    </button>
                    <button class="btn btn-secondary me-2" @click="goToEditTask(task.id, story.id)">
                      Edit task
                    </button>
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToChangeStateTask(task.id, story.id)"
                    >
                      Edit state
                    </button>
                    <button class="btn btn-danger" @click="goToDeleteTask(task.id, story.id)">
                      Delete task
                    </button>
                  </div>
                </div>
              </template>

              <template #done>
                <div v-for="task in tasksList(story.id, 'DONE')" :key="task.id" class="task-item">
                  <div class="task-name">{{ task.name }}</div>
                  <div class="task-buttons">
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToDetailsTask(task.id, story.id)"
                    >
                      Details
                    </button>
                    <button
                      class="btn btn-secondary me-2"
                      @click="goToChangeStateTask(task.id, story.id)"
                    >
                      Edit state
                    </button>
                    <button class="btn btn-danger" @click="goToDeleteTask(task.id, story.id)">
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

<style lang="css">
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

.task-name {
  font-weight: bold;
  margin-bottom: 10px;
  font-size: 16px;
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
</style>

<script setup lang="ts">
import type { StoryInterface } from '@/lib/domain/interfaces/storyInterface'
import { useRoute, useRouter } from 'vue-router'
import mainLayout from '@/lib/presentation/layouts/mainLayout.vue'
import StoryService from '@/lib/application/services/storyService'
import { ref, onMounted } from 'vue'
import taskLayout from '@/lib/presentation/layouts/taskLayout.vue'
import TaskService from '@/lib/application/services/taskService'

const route = useRoute()
const router = useRouter()

const projectId = Number(route.params.projectId)
const toogleDetails = ref<number | null>(null)

const storiesService = new StoryService()
const taskService = new TaskService()

const storiesList = ref<StoryInterface[] | null>([])

const goToCreateStory = () => {
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
const goToCreateTask = (storyId: number) => {
  router.push({ name: 'TaskCreate', params: { projectId: projectId, storyId: storyId } })
}
const goToDeleteTask = (taskId: number, storyId: number) => {
  router.push({
    name: 'TaskDelete',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}
const goToDetailsTask = (taskId: number, storyId: number) => {
  router.push({
    name: 'TaskDetails',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}
const goToEditTask = (taskId: number, storyId: number) => {
  router.push({
    name: 'TaskEdit',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}

const goToChangeStateTask = (taskId: number, storyId: number) => {
  router.push({
    name: 'TaskChangeState',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}

const goToAssignUserChangeStateTask = (taskId: number, storyId: number) => {
  router.push({
    name: 'TaskChangeStateAssignUser',
    params: { projectId: projectId, storyId: storyId, taskId: taskId },
  })
}

onMounted(() => {
  fetchStoriesList()
})

const fetchStoriesList = () => {
  storiesList.value = storiesService.GetStoriesList(projectId)
}
const toggleDetails = (storyId: number) => {
  toogleDetails.value = toogleDetails.value === storyId ? null : storyId
}

const tasksList = (storyId: number, state: string) =>
  taskService.GetSpecifiedTasksList(storyId, state)
</script>
