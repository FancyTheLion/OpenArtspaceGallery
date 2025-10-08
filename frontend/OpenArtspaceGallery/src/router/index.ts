import HomeView from "../views/HomeView.vue";
import {createRouter, createWebHistory} from "vue-router";
import AlbumContentView from "../views/Albums/AlbumContentView.vue";
import AdminPanelView from "../views/AdminPanel/AdminPanelView.vue";
import ImagePageView from "../views/Images/ImagePageView.vue";

const routes = [
    // Main page
    {
        path: "/",
        name: "homePage",
        component: HomeView
    },

    // Album contents
    {
        path: "/albums/:currentAlbumId",
        name: "albumContent",
        component: AlbumContentView,
        props: true
    },

    // Redirect to home when album id is missing
    {
        path: '/albums',
        redirect: '/'
    },

    // Admin panel
    {
        path: "/adminPanel",
        name: "adminPanel",
        component: AdminPanelView
    },

    // Images contents
    {
        path: "/images/:imageId",
        name: "viewImage",
        component: ImagePageView,
        props: true
    },

    // Redirect to home when album id is missing
    {
        path: '/images',
        redirect: '/'
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
