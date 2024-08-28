import '@babel/polyfill'
import 'mutationobserver-shim'
import Vue from 'vue'
import './plugins/fontawesome'
import './plugins/bootstrap-vue'
import App from './App.vue'
import router from './router'
import store from './store'
//import vuetify from './plugins/vuetify'
import './plugins/axios'
//import './style.css'
import 'font-awesome/css/font-awesome.css'
import './config/toasted'

Vue.config.productionTip = false

Vue.filter('formatDate', function(value) {
  console.log('value', value)
  const date = new Date(value).toLocaleDateString()
  console.log('date', date)
  return date
})

new Vue({
  router,
  store,
  //vuetify,
  render: h => h(App)
}).$mount('#app')
