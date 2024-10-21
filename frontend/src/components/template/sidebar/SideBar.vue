<template>
    <div class="sidebar" :style="{width: sidebarWidth }">
        <!-- <h1>
           <span v-if="collapsed">
            <div>V</div>
            <div>S</div>
           </span>
           <span v-else>Vue Sidebar</span> 
        </h1> -->
        <SideBarLink :to="'/'" :icon="'fa fa-home fa-2x'">&nbsp;Home</SideBarLink><br>
        <SideBarLink :to="'/users'" :icon="'fa fa-users fa-2x'">&nbsp;Users</SideBarLink><br>
        <SideBarLink :to="'/projects'" :icon="'fa fa-tasks fa-2x'">&nbsp;Projects</SideBarLink><br>
        <SideBarLink :to="'/charts'" :icon="'fa fa-bar-chart fa-2x'">&nbsp;Charts</SideBarLink><br>
        <span 
            class="collapse-icon"
            :class="{'rotate-180': collapsed }"
            @click="toggleSidebar"
        >
            <i class="fa fa-angle-double-left"></i>
        </span>
    </div>
</template>

<script>
import SideBarLink from './SideBarLink.vue';
import { collapsed, toggleSidebar, sidebarWidth } from '@/store/state';
    export default {
        name: 'SideBar',
        components: { SideBarLink },
        data() {
            return {
                collapsed, toggleSidebar, sidebarWidth
            }
        }
    }
</script>

<style>
:root {
    --sidebar-bg-color: #2f855a;
    --sidebar-item-hover: #38a169;
    --sidebar-item-active: #276749;
}
</style>

<style scoped> 
    .sidebar {
        color: white;
        background-color: var(--sidebar-bg-color);

        float: left;
        position: fixed;
        z-index: 1;
        top: 65px;
        left: 0;
        bottom: 0;
        padding: 0.5em;

        transition: 0.3s ease;

        display: flex;
        flex-direction: column;
    }

    .collapse-icon {
        position: absolute;
        bottom: 0;
        padding: 0.75em;

        color: rgba(255, 255, 255, 0.7);
        transition: 0.2s linear;
    }

    .rotate-180 {
        transform: rotate(180deg);
        transition: 0.2s linear;
    }
</style>