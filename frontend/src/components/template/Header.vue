<template>
    <v-app-bar app>
        <v-toolbar-title  class="headline text-uppercase mr-4">
            <span>Dev</span>
            <span class="font-weight-light">Freela</span>
        </v-toolbar-title>
        <v-toolbar-items>
            <v-btn text to="/" class="teste">Home</v-btn>
            <v-btn text to="/users">Users</v-btn>
            <v-btn text to="/Projects">Projects</v-btn>
        </v-toolbar-items>
    </v-app-bar>
</template>

<script>
export default {
    name: 'Header'
}
</script>
