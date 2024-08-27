<template>
    <nav class="menu mt-5">
        <ul>
            <router-link to="/" tag="li" active-class="active" exact>
              <a>Home</a>
            </router-link>
            <router-link to="/users" tag="li" active-class="active">
              <a>User</a>
            </router-link>
            <router-link to="/projects" tag="li" active-class="active">
              <a>Projects</a>
            </router-link>
        </ul>
    </nav>
</template>

<script>
    export default {
        name: 'Menu'
    }
</script>

<style scoped>

</style>