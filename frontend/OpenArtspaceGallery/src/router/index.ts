import HomeView from "../views/HomeView.vue";
import {createRouter, createWebHistory} from "vue-router";
import AlbumContentView from "../views/AlbumContentView.vue";

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
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
