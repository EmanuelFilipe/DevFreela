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

      if(user) {
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
