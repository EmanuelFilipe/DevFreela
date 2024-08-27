/* eslint-disable */ 
import Vue from "vue"
import axios from 'axios'

axios.defaults.baseURL = 'https://localhost:7144/api'

Vue.use({
    install(Vue) {
        Vue.prototype.$http = axios
    }
})