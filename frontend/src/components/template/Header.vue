<template>
    <v-app-bar app style="background-color: #f8f9fa;">
        <v-toolbar-title  class="headline text-uppercase mr-4">
            <span>Dev</span>
            <span class="font-weight-light">Freela</span>
        </v-toolbar-title>
        <v-toolbar-items v-if="isLoggedIn">
                <v-btn text to="/">Home</v-btn>
                <v-btn text to="/users">Users</v-btn>
                <!-- <v-btn text to="/Projects">Projects</v-btn> -->
        </v-toolbar-items>
        <v-spacer></v-spacer>
        <v-toolbar-items>
            <UserDropdown v-if="isLoggedIn" />
        </v-toolbar-items>
    </v-app-bar>
</template>

<script>
import { mapGetters } from 'vuex';
import UserDropdown from './UserDropdown.vue';
export default {
    name: 'Header',
    components: { UserDropdown },
    props: { 
        hideUserDropdown: Boolean
    },
    computed: {
        ...mapGetters(['isLoggedIn'])
    }
}
</script>

<style scoped>
a {
    text-decoration: none;
}
</style>
