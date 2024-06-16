import HomeView from "../views/HomeView.vue";
import {createRouter, createWebHistory} from "vue-router";

const routes = [
    // Main page
    {
        path: '/',
        name: 'homePage',
        component: HomeView
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
