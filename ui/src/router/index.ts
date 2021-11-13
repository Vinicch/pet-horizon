import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import BaseApp from '@/views/Base.vue'
import Login from '@/views/Login.vue'
import Adoptions from '@/views/ONG/Adoptions.vue'
import Ongs from '@/views/ONG/ONGs.vue'
import Pets from '@/views/ONG/Pets.vue'
import Users from '@/views/ONG/Users.vue'
import Search from '@/views/client/Search.vue'
import Requests from '@/views/client/Requests.vue'

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
        component: Adoptions,
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
