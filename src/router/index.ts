import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/project/details/:id',
      name: 'ProjectDetals',
      component: () => import('@/views/ProjectViews/ProjectDetails.vue'),
    },
    {
      path: '/project/edit/:id',
      name: 'ProjectEdit',
      component: () => import('@/views/ProjectViews/ProjectEdit.vue'),
    },
    {
      path: '/project/delete/:id',
      name: 'ProjectDelete',
      component: () => import('@/views/ProjectViews/ProjectDelete.vue'),
    },
    {
      path: '/project/create',
      name: 'ProjectCreate',
      component: () => import('@/views/ProjectViews/ProjectCreate.vue'),
    },
    {
      path: '/project/list',
      name: 'ProjectList',
      component: () => import('@/views/ProjectViews/ProjectList.vue'),
    },
  ],
})

export default router
