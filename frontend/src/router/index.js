import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Auth from '@/views/Auth/Auth.vue'
import AdminPages from '@/components/admin/AdminPages.vue'
import Users from '@/views/Users/Users.vue'
import User from '@/views/Users/User.vue'
import Projects from '@/views/Projects/Projects.vue'
import Project from '@/views/Projects/Project.vue'
import { userKey } from '@/global'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: "/admin",
    name: "adminPages",
    component: AdminPages,
    meta: { requiresAdmin: true }
  },
  {
    path: '/users',
    name: 'users',
    component: Users
  },
  {
    path: '/user',
    name: 'user',
    component: User
  },
  {
    path: '/user/:id',
    name: 'user-edit',
    component: User
  },
  {
    path: '/projects',
    name: 'projects',
    component: Projects
  },
  {
    path: '/project',
    name: 'project-create',
    component: Project
  },
  {
    path: '/project/:id',
    name: 'project-edit',
    component: Project
  },
  {
    name: 'auth',
    path: '/auth',
    component: Auth
  }]

const router = new VueRouter({
  mode: 'history',
  //base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
  const json = localStorage.getItem(userKey)
  if(to.matched.some(record => record.meta.requiresAdmin)) {
      const user = JSON.parse(json)
      user && user.isAdmin ? next() : next({ path: '/' })
  } else {
      next()
  }
})

export default router
