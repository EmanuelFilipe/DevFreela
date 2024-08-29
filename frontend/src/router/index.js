import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Users from '@/views/Users/Users.vue'
import User from '@/views/Users/User.vue'
import Projects from '@/views/Projects/Projects.vue'
import Auth from '@/views/Auth/Auth.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
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
    name: 'auth',
    path: '/auth',
    component: Auth
  }]

const router = new VueRouter({
  mode: 'history',
  //base: process.env.BASE_URL,
  routes
})

export default router
