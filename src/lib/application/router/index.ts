import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/lib/presentation/views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
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
  ],
})

export default router
