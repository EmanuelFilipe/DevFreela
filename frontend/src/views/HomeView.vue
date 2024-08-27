<template>
  <b-container>
    <b-row>
      <b-col>
        <h1 class="text-center">Bem-vindo ao Sistema de Freelancers</h1>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
export default {
  name: 'Home',
  data () {
    return {
      users: {}  // Inicializa como um objeto
    }
  },
  methods: {
    loadUsers () {
      this.$http.get('/users/2')
                .then(res => {
                  this.users = res.data  // Define o objeto retornado
                })
    }
  },
  mounted() {
    this.loadUsers()
  }
}
</script>
