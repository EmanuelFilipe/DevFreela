<template>
    <div class="user-dropdown">
        <div class="user-button">
            <span style="color: black; font-weight: 500" class="d-none d-sm-block">{{ user.name }}</span>
            <div class="user-dropdown-img">
                <Gravatar :email="user.email" alt="user" />
            </div>
            <i style="color: black" class="fa fa-angle-down"></i>
        </div>
        <div class="user-dropdown-content">
            <router-link to="/admin" v-if="user.admin">
                <i class="fa fa-cogs"></i>Administration
            </router-link>
            <a href @click.prevent="logout">
                <i class="fa fa-sign-out"></i>Logout
            </a>
        </div>
    </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex';
import Gravatar from 'vue-gravatar'
import { userKey } from '@/global';

    export default {
        name: 'UserDropdown',
        components: { Gravatar },
        computed: mapState(['user']), ...mapGetters(['isLoggedIn']),
        methods: {
            logout() {
                localStorage.removeItem(userKey)
                this.$store.commit('setUser', null)
                this.$router.push({ path: '/auth'})
            }
        }
    }
</script>

<style scoped>
.user-dropdown {
    height: 100%;
    /* will be change by user-dropdown-content */
    position: relative;

}
    .user-dropdown:hover {
        background-color: rgba(0,0,0, 0.2);
    }

.user-button {
    display: flex;
    align-items: center;
    color: #fff;
    font-weight: 100;
    height: 100%;
    padding: 10px 10px;
}

.user-dropdown-img {
    margin: 0 10px;
}

.user-dropdown-img > img {
    max-height: 45px;
    border-radius: 5px;
}

.user-dropdown-content {
    position: absolute;
    right: 0px;
    background-color: #f9f9f9;
    min-width: 180px;
    box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
    padding: 10px;
    z-index: 1;

    display: flex;
    flex-direction: column;
    flex-wrap: wrap;

    visibility: hidden;
    opacity: 0;
    transition: visibility 0s, opacity 0.5s linear;
}

.user-dropdown-content a {
    text-decoration: none;
    color: #000;
    padding: 10px;
}

.user-dropdown-content a i {
    padding-right: 10px;
    font-weight: bold;
}

.user-dropdown-content a:hover {
background-color: #ededed;
}

/* quando estiver com o mouse em cima, irá ser aplicado uma nova regra dentro do content*/
.user-dropdown:hover .user-dropdown-content {
    visibility: visible;
    opacity: 1;
}
</style>