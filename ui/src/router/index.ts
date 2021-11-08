import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import BaseApp from '@/views/Base.vue'
import Dashboard from '@/views/Dashboard.vue'
import Login from '@/views/Login.vue'
import Ongs from '@/views/ONGs.vue'
import Pets from '@/views/Pets.vue'
import Users from '@/views/Users.vue'
import Search from '@/views/Search.vue'
import Requests from '@/views/Requests.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'BaseApp',
    component: BaseApp,
    meta: {
      requiresAuth: true,
    },
    children: [
      {
        path: '/dashboard',
        name: 'Dashboard',
        component: Dashboard,
      },
      {
        path: 'ongs',
        name: 'ONGs',
        component: Ongs,
      },
      {
        path: 'pets',
        name: 'Pets',
        component: Pets,
      },
      {
        path: 'users',
        name: 'Users',
        component: Users,
      },
      {
        path: 'search',
        name: 'Search',
        component: Search,
      },
      {
        path: 'requests',
        name: 'Requests',
        component: Requests,
      },
    ],
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: '/:pathMatch(.*)*',
    redirect: '/',
    meta: {
      requiresAuth: true,
    },
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

router.beforeEach((to, _, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    const userData = localStorage.getItem('user')

    if (!userData) next({ path: 'login' })
    else next()
  } else {
    next()
  }
})

export default router
