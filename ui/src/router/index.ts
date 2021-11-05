import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Dashboard from '../views/Dashboard.vue'
import Ongs from '../views/ONGs.vue'
import Pets from '../views/Pets.vue'
import Users from '../views/Users.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Dashboard',
    component: Dashboard,
  },
  {
    path: '/ongs',
    name: 'ONGs',
    component: Ongs,
  },
  {
    path: '/pets',
    name: 'Pets',
    component: Pets,
  },
  {
    path: '/users',
    name: 'Users',
    component: Users,
  },
  {
    path: '/:pathMatch(.*)*',
    redirect: '/',
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
