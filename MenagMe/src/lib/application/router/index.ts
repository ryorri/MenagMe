import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/lib/presentation/views/HomeView.vue'
import LoginPage from '@/lib/presentation/views/LoginPage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: LoginPage,
    },
    {
      path: '/home',
      name: 'home',
      component: HomeView,
    },
    ////////Project/////////////////////////////////////////////////////////////////
    {
      path: '/project/details/:id',
      name: 'ProjectDetals',
      component: () => import('@/lib/presentation/views/ProjectViews/ProjectDetails.vue'),
    },
    {
      path: '/project/edit/:id',
      name: 'ProjectEdit',
      component: () => import('@/lib/presentation/views/ProjectViews/ProjectEdit.vue'),
    },
    {
      path: '/project/delete/:id',
      name: 'ProjectDelete',
      component: () => import('@/lib/presentation/views/ProjectViews/ProjectDelete.vue'),
    },
    {
      path: '/project/create',
      name: 'ProjectCreate',
      component: () => import('@/lib/presentation/views/ProjectViews/ProjectCreate.vue'),
    },
    {
      path: '/project/list',
      name: 'ProjectList',
      component: () => import('@/lib/presentation/views/ProjectViews/ProjectList.vue'),
    },
    ////////Stories/////////////////////////////////////////////////////////////////
    {
      path: '/story/details/:projectId/:storyId',
      name: 'StoryDetails',
      component: () => import('@/lib/presentation/views/StoryViews/StoryDetails.vue'),
    },
    {
      path: '/story/edit/:projectId/:storyId',
      name: 'StoryEdit',
      component: () => import('@/lib/presentation/views/StoryViews/StoryEdit.vue'),
    },
    {
      path: '/story/delete/:projectId/:storyId',
      name: 'StoryDelete',
      component: () => import('@/lib/presentation/views/StoryViews/StoryDelete.vue'),
    },
    {
      path: '/story/create/:projectId',
      name: 'StoryCreate',
      component: () => import('@/lib/presentation/views/StoryViews/StoryCreate.vue'),
    },
    {
      path: '/story/list/:projectId',
      name: 'StoryList',
      component: () => import('@/lib/presentation/views/StoryViews/StoryList.vue'),
    },
    /////////Tasks////////////////////////////////////////////////////////////////////////
    {
      path: '/task/create/:projectId/:storyId',
      name: 'TaskCreate',
      component: () => import('@/lib/presentation/views/TaskViews/TaskCreate.vue'),
    },
    {
      path: '/task/edit/:projectId/:storyId/:taskId',
      name: 'TaskEdit',
      component: () => import('@/lib/presentation/views/TaskViews/TaskEdit.vue'),
    },
    {
      path: '/task/create/:projectId/:storyId/:taskId',
      name: 'TaskDetails',
      component: () => import('@/lib/presentation/views/TaskViews/TaskDetails.vue'),
    },
    {
      path: '/task/edit/:projectId/:storyId/:taskId',
      name: 'TaskDelete',
      component: () => import('@/lib/presentation/views/TaskViews/TaskDelete.vue'),
    },
    {
      path: '/task/changestate/:projectId/:storyId/:taskId',
      name: 'TaskChangeStateAssignUser',
      component: () => import('@/lib/presentation/views/TaskViews/TaskAssignUserChangeState.vue'),
    },
    {
      path: '/task/changestate/:projectId/:storyId/:taskId',
      name: 'TaskChangeState',
      component: () => import('@/lib/presentation/views/TaskViews/TaskChangeState.vue'),
    },
    /////////////////////////////////////////////////////////////////////////////////////////
    {
      path: '/noproject',
      name: 'NoProject',
      component: () => import('@/lib/presentation/views/NoProject.vue'),
    },
  ],
})

export default router
