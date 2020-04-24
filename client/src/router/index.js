import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

  const routes = [
    // {
    //   path: '',
    //   name: 'App',
    //   component: () => import("@/App.vue")
    // },
    {
      path: '/reg',
      name: 'Reg',
      component: () => import("../components/CheckIn.vue")
    },
    {
      path: '/signin',
      name: 'SignIn',
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
      component: () => import("../components/CreatePoll.vue")
    }


    
    // path: '/',
    // name: 'Home',
    // component: Home
  
    // path: '/test',
    // name: 'test',
    // component: () => import("../components/test.vue")
  
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