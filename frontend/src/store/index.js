import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    user: null,
    isLoggedIn: !!localStorage.getItem('userKey')
  },
  getters: {
    isLoggedIn: state => state.isLoggedIn
  },
  mutations: {
    setLoginState(state, payload) {
      state.isLoggedIn = payload
    },
    setUser(state, user) {
      state.user = user
      console.log('state.user', state.user )
      if(user) {
        console.log('setUser:user.token', user.token)
        axios.defaults.headers.common['Authorization'] = `bearer ${user.token}`
        state.isLoggedIn = true
      } else {
        delete axios.defaults.headers.common['Authorization']
        state.isLoggedIn = false
      }
  }
  },
  actions: {
    login({ commit }) {
      commit('setLoginState', true)
    }
  },
  modules: {
  }
})
