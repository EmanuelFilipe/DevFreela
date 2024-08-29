<template>
  <div class="auth-content">
    <div class="auth-modal">
      <!-- <img src="../../assets/logo.png" alt="logo" width="200"> -->
      <hr />
      <div class="auth-title">
        Login
      </div>
      
      <input
        v-model="user.email"
        name="email"
        placeholder="E-mail"
        type="text"
        @keyup.enter="signin"
        required
      />
      <input
        v-model="user.password"
        name="password"
        placeholder="Password"
        type="password"
        @keyup.enter="signin"
        required
      />
      <button @click="signin">Enter</button>
      <a @click="signup">
        <span v-if="showSignup">Ja tem cadastro? Acesse o Login!</span>
        <span v-else>Não tem cadastro? Registre-se aqui!</span>
      </a>
    </div>
  </div>
</template>

<script>
import { mapActions } from "vuex";
import { userKey } from "@/global";
import { isEmptyOrNull } from "@/utils/validators";
//import axios from "axios";
export default {
  name: "Auth",
  data() {
    return {
      showSignup: false,
      user: {},
      token: ''
    };
  },
  methods: {
    ...mapActions(['login']),
    signup() {
      this.$router.push({ path: "/user" });
    },
    signin() {

      const resultEmail = isEmptyOrNull('Email', this.user.email)
      if (resultEmail) {
        this.$toasted.global.defaultError({ msg: resultEmail })
        return
      }

      const resultPassword = isEmptyOrNull('Password', this.user.password)
      if (resultPassword) {
        this.$toasted.global.defaultError({ msg: resultPassword })
        return
      }

      this.$http
        .put("/users/login", this.user)
        .then((res) => {
          this.$store.commit('setUser', res.data)
          localStorage.setItem(userKey, JSON.stringify(res.data));
          this.login();
          this.$router.push({ path: "/" });
        })
        .catch(error => {
            this.$toasted.global.defaultError({ msg: error?.message || '' })
        });
    },
  },
};
</script>

<style scoped>
.auth-content {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.auth-modal {
  background-color: #fff;
  width: 350px;
  padding: 35px;
  box-shadow: 0 1px 5px rgba(0, 0, 0, 0.15);

  display: flex;
  flex-direction: column;
  align-items: center;
}

.auth-modal input {
  border: 1px solid #bbb;
  width: 100%;
  margin-bottom: 15px;
  padding: 3px 8px;
  outline: none;
}

.auth-modal button {
  align-self: flex-end;
  background-color: #2460ae;
  color: #fff;
  padding: 5px 15px;
}

.auth-modal a {
  margin-top: 35px;
}

.auth-modal hr {
  border: 0;
  width: 100%;
  height: 1px;
  background-image: linear-gradient(
    to right,
    rgba(120, 120, 120, 0),
    rgba(120, 120, 120, 0.75),
    rgba(120, 120, 120, 0)
  );
}

.auth-title {
  font-size: 1.2rem;
  font-weight: bold;
  margin-top: 10px;
  margin-bottom: 15px;
}
</style>
