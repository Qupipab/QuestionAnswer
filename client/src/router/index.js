import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

  const routes = [
    {
      path: '/reg',
      name: 'Reg',
      component: () => import("../components/CheckIn.vue")
    },
    {
      path: '/signin',
      name: 'SignIn',
      props: true,
      component: () => import("../components/Auth.vue")
    },
    {
      path: '/main',
      name: 'Main',
      component: () => import("../components/Main.vue")
    },
    {
      path: '/cabinet',
      name: 'Cabinet',
      component: () => import("../components/Cabinet.vue")
    },
    {
      path: '/createpoll',
      name: 'CreatePoll',
      props: true,
      component: () => import("../components/CreatePoll.vue")
    },
    {
      path: '/main/poll/:id',
      name: 'Poll',
      props: true,
      component: () => import("../components/Poll.vue")
    },
    {
      path: '/poll/:id/VotedUsers',
      name: 'VotedUsers',
      props: true,
      component: () => import("../components/VotedUsers.vue")
    }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router

    // path: '/about',
    // name: 'About',
    // component: Home
    // // route level code-splitting
    // // this generates a separate chunk (about.[hash].js) for this route
    // // which is lazy-loaded when the route is visited.
    // component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')