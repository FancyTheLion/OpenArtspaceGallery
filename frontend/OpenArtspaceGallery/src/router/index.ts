import HomeView from "../views/HomeView.vue";
import {createRouter, createWebHistory} from "vue-router";
import AlbumContentView from "../views/AlbumContentView.vue";
import AdminPanelView from "../views/AdminPanelView.vue";

const routes = [
    // Main page
    {
        path: "/",
        name: "homePage",
        component: HomeView
    },

    // Album contents
    {
        path: "/albums/:currentAlbumId?",
        name: "albumContent",
        component: AlbumContentView,
        props: true
    },

    // Admin panel
    {
        path: "/adminPanel",
        name: "adminPanel",
        component: AdminPanelView
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
