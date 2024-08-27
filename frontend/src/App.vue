<template>
  <div id="app">
    <Menu />
    <transition mode="out-in"
		enter-active-class="animated rollIn"
		leave-active-class="animated rollOut"
		>
		<router-view />
	</transition>
  </div>
</template>

<script>
/* eslint-disable */ 
import Menu from '@/components/template/Menu.vue';

export default {
  name: 'App',
  components: { Menu }
}
</script>

<style lang="scss">
#app {
  /*display: flex;
  flex-direction: column;
  align-items: center;
  color: #fff;*/
  margin: 50px;
  padding: 0px;
}

</style>
