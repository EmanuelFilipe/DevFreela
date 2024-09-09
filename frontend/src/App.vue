<template>
  <v-app>
    <Header />
    <v-main class="main">
      <v-container class="mt-6" >
        <transition name="slide" mode="out-in">
          <router-view />
        </transition>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
/* eslint-disable */
import Header from "./components/template/Header.vue";
import { userKey } from "@/global";
import axios from "axios";
import { mapState } from "vuex";

export default {
  name: "App",
  components: { Header },
  data: function() {
		return {
			validatingToken: true
		}
	},
  methods: {
    async validateToken() {
      this.validatingToken = true;

      const json = localStorage.getItem(userKey)
      const userData = JSON.parse(json)
      this.$store.commit('setUser', null)

      if (!userData) {
        delete axios.defaults.headers.common["Authorization"];
        this.validatingToken = false;
        this.$router.push({ name: "auth" });
        return;
      }

      if (userData) {
        this.$store.commit('setUser', userData)
      } else {
        localStorage.removeItem(userKey);
        this.$router.push({ name: "auth" });
      }

      this.validatingToken = false;
    },
  },
  created() {
		this.validateToken()
	},
  computed: mapState(['user'])
};
</script>

<style>
.main {
  background-color: #fdfdfdea;
}

@keyframes slide-in {
  from {
    transform: translateY(-30px);
    opacity: 0;
  }
  to {
    transform: translateY(0px);
    opacity: 1;
  }
}

@keyframes slide-out {
  from {
    transform: translateY(0px);
    opacity: 1;
  }
  to {
    transform: translateY(-30px);
    opacity: 0;
  }
}

.slide-enter-active {
  animation: slide-in 0.3s ease;
}

.slide-leave-active {
  animation: slide-out 0.3s ease;
}

.pagination {
    display: flex;
    flex-direction: row;
    justify-content: center;
  }
</style>
