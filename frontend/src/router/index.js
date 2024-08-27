import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Users from '@/views/Users/Users.vue'
import UserCreate from '@/views/Users/UserCreate.vue'
import Projects from '@/views/Projects/Projects.vue'
import Teste from '@/views/Users/Teste.vue'

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
    path: '/user/create',
    name: 'user-create',
    component: UserCreate
  },
  {
    path: '/user/create/:id',
    name: 'user-create-id',
    component: UserCreate
  },
  {
    path: '/teste',
    name: 'teste',
    component: Teste
  },
  {
    path: '/projects',
    name: 'projects',
    component: Projects
  },
  
  
]

const router = new VueRouter({
  mode: 'history',
  //base: process.env.BASE_URL,
  routes
})

export default router
