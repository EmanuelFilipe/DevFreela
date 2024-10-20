import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Auth from '@/views/Auth/Auth.vue'
import AdminPages from '@/components/admin/AdminPages.vue'
import Users from '@/views/Users/Users.vue'
import User from '@/views/Users/User.vue'
import Projects from '@/views/Projects/Projects.vue'
import Project from '@/views/Projects/Project.vue'
import Charts from '@/views/Charts.vue'
import { userKey } from '@/global'
import { app } from '../main';

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
    component: Projects,
    meta: { requiresAuth: true, requiredRoles: ['admin', 'client'] }
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
  },
  {
    name: 'charts',
    path: '/charts',
    component: Charts
  },
]

const router = new VueRouter({
  mode: 'history',
  //base: process.env.BASE_URL,
  routes
})

// router.beforeEach((to, from, next) => {
//   const json = localStorage.getItem(userKey)
//   console.log('router:json', json)
//   if(to.matched.some(record => record.meta.requiresAdmin)) {
//       const user = JSON.parse(json)
//       console.log('router:user', user)
//       user && user.isAdmin ? next() : next({ path: '/' })
//   } else {
//       next()
//   }
// })

// /* eslint-disable */
// router.beforeEach((to, from, next) => {
//   const user = JSON.parse(localStorage.getItem(userKey));
//   console.log('router:json', user)
//   // Verifique se a rota requer acesso de admin
//   if (to.matched.some(record => record.meta.requiresAdmin)) {
//       console.log('router:record.meta', record.meta)
//       // Verifique se o usuário está logado e tem as permissões
//       if (user && user.token && user.isAdmin) {
//           next(); // Permite a navegação
//       } else {
//           next({ path: '/not-authorized' }); // Redireciona para uma página de não autorizado
//       }
//   } else {
//       next(); // Permite a navegação
//   }
// });

// router.beforeEach((to, from, next) => {
//   const user = JSON.parse(localStorage.getItem(userKey));
//   const requiredRoles = to.meta.requiredRoles;
//   const userRole = user.role;

//   if (requiredRoles) {
//     if (requiredRoles.includes(userRole)) {
//       next();
//     } else {
//       alert('Acesso negado: você não tem a permissão necessária.');
//       next('/'); // Redireciona para a página de login ou outra página
//     }
//   } else {
//     next(); // Se não houver roles específicas requeridas, simplesmente prossegue
//   }
// });

router.beforeEach((to, from, next) => {
  const user = JSON.parse(localStorage.getItem(userKey));
  const requiresAuth = to.meta.requiresAuth;
  const requiredRoles = to.meta.requiredRoles;

  if (!user && requiresAuth) {
    app.$toasted.global.defaultError({ msg: 'You need to be logged in to access this page.' });
    next('/auth');
  } else if (user && requiredRoles) {
    const userRole = user?.role; // Verifica se o usuário existe antes de acessar a role
    if (userRole && requiredRoles.includes(userRole)) {
      next();
    } else {
      app.$toasted.global.defaultError({ msg: 'Access Denied: You do not have the required permission.' });
      next('/');
    }
  } else {
    next(); // Se não houver roles específicas requeridas, simplesmente prossegue
  }
});

export default router
