<template>
  <b-container>
    <b-row>
      <b-col class="mt-16">
        <h1 id="home-letter" class="text-center">Welcome to the Freelancer System</h1>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
//import { showError, userKey } from '@/global';
//import axios from 'axios';
export default {
  name: 'Home',
  data() {
    return {
      token: ''
    }
  },
  // created() {
  //   let user = {
  //     email: 'teste3@teste.com',
  //     password: 'Aa12345!'
  //   }
  //   this.$http.put('/users/login', user)
  //   .then(res => {
  //     this.token = res.data.token
  //     axios.defaults.headers.common['Authorization'] = `bearer ${this.token}`
  //     localStorage.setItem(userKey, JSON.stringify(this.token))
  //   })
  //   .catch(() => {
  //     showError,
  //     delete axios.defaults.headers.common['Authorization'] 
  //   })
  // }
}
</script>
