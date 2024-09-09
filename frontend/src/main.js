import '@babel/polyfill'
import 'mutationobserver-shim'
import Vue from 'vue'
import './plugins/fontawesome'
import './plugins/bootstrap-vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import './plugins/axios'
//import './style.css'
import 'font-awesome/css/font-awesome.css'
import '@/config/toasted'

Vue.config.productionTip = false

Vue.filter('formatDate', function(value) {
  if (!value) return ''
  const date = new Date(value).toLocaleDateString()
  return date
})

Vue.filter('formatDateTime', function(value) {
  if (!value) return ''
  const date = new Date(value)
  const formattedDate = date.toLocaleDateString()
  const formattedTime = date.toLocaleTimeString()
  return `${formattedDate} ${formattedTime}`
})

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
