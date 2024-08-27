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
import './style.css'
import 'font-awesome/css/font-awesome.css'

Vue.config.productionTip = false

new Vue({
  router,
  store,
  //vuetify,
  render: h => h(App)
}).$mount('#app')
