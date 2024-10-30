<template>
  <v-app>
    <Header />
    <!-- <SideBar /> -->
    <v-main class="main" >
      <v-container class="mt-6" >
        <transition name="slide" mode="out-in">
          <div :style="{'margin-left:': sidebarWidth}">
            <router-view />
          </div>
        </transition>
      </v-container>
    </v-main>
    <Footer />
  </v-app>
</template>

<script>
/* eslint-disable */
import Header from "./components/template/Header.vue";
import Footer from "./components/template/Footer.vue";
// import SideBar from "./components/template/sidebar/SideBar.vue";
import { sidebarWidth } from "./store/state";
import { userKey } from "@/global";
import axios from "axios";
import { mapState } from "vuex";

export default {
  name: "App",
  components: { Header, Footer },
  data: function() {
		return {
			validatingToken: true,
      sidebarWidth
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
</style>
